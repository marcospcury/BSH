using BitShareData;
using BitSharePortal.Models;
using MonoTorrent.BEncoding;
using MonoTorrent.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace BitSharePortal.Controllers
{
    [Authorize]
    public class TorrentsController : BitShareController
    {
        private List<SelectListItem> ListaCategoria = new List<SelectListItem>();

        private string queryBase = "select t.IdTorrent, t.Nome, t.Tamanho, t.Seeds, t.Leechers, t.DataLancamento, 0 as Downloads, u.Nome as UsuarioLancamento From Torrents t Inner Join Usuarios u on u.IdUsuario = t.UsuarioLancamento_IdUsuario";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Browse(TorrentFilterModel filtro)
        {
            ViewBag.ListaCategoria = CarregarListaCategoria();

            var listaTorrents = Connection.ExecuteQuery<TorrentModel>(queryBase);
            ViewBag.ListaTorrents = listaTorrents;

            return View(filtro);
        }

        public ActionResult Browse()
        {
            ViewBag.ListaCategoria = CarregarListaCategoria();

            var listaTorrents = Connection.ExecuteQuery<TorrentModel>(queryBase);
            ViewBag.ListaTorrents = listaTorrents;

            return View();
        }

        public ActionResult Detail(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase uploadFile, TorrentUploadModel model)
        {
            ViewBag.ListaCategoria = CarregarListaCategoria();

            if (ModelState.IsValid)
            {
                var arquivoTorrent = String.Format("{0}\\{1}", Server.MapPath("/Torrents"), uploadFile.FileName);
                uploadFile.SaveAs(arquivoTorrent);
                var dadosTorrent = MonoTorrent.Common.Torrent.Load(arquivoTorrent);
                Usuario usuarioLancamento;

                using (var repositorio = new DataRepository<BitShareData.Torrent>())
                {
                    var detalhe = new DetalheTorrent();

                    detalhe.IdDetalheTorrent = 0;
                    detalhe.Descricao = model.Descricao;
                    detalhe.Imagens = model.ImagensURL;

                    var bitShareTorrent = new BitShareData.Torrent();
                    bitShareTorrent.IdTorrent = 0;
                    bitShareTorrent.Nome = uploadFile.FileName; // Trocar por nome e criar um campo de arquivo
                    bitShareTorrent.Categoria = model.Categoria;
                    bitShareTorrent.HashInfo = dadosTorrent.InfoHash.ToString();
                    bitShareTorrent.Tamanho = dadosTorrent.Size;
                    bitShareTorrent.Seeds = 0;
                    bitShareTorrent.Leechers = 0;
                    bitShareTorrent.DataLancamento = DateTime.Now;
                    bitShareTorrent.FreeLeech = false;
                    bitShareTorrent.Ativo = true;
                    bitShareTorrent.PrimeiroSnatch = false;
                    bitShareTorrent.DetalheTorrent = detalhe;

                    foreach (var file in dadosTorrent.Files)
                    {
                        var arquivo = new ArquivoTorrent();
                        arquivo.Nome = file.Path;
                        arquivo.Tamanho = file.Length;

                        bitShareTorrent.ArquivoTorrents.Add(arquivo);
                    }

                    // recupera o usuário pela entity, para poder fazer o relacionamento com o Torrent
                    using (var repositorioUsuario = new DataRepository<BitShareData.Usuario>(repositorio.Context))
                    {
                        usuarioLancamento = repositorioUsuario.First(u => u.IdUsuario == UsuarioLogado.IdUsuario);

                        bitShareTorrent.UsuarioLancamento = usuarioLancamento;

                        repositorio.Add(bitShareTorrent);
                        repositorio.SaveChanges();
                    }
                }
            }

            return View(model);
        }

        public ActionResult Upload()
        {
            ViewBag.ListaCategoria = CarregarListaCategoria();
            return View();
        }

        public FileResult Download(int id)
        {
            try
            {
                var torrent = Connection.ExecuteQuery<BitShareData.Torrent>(String.Format("Select Nome from Torrents where IdTorrent = {0}", id.ToString())).First();

                byte[] buffer;
                using (var torrentStream = System.IO.File.Open(String.Format("{0}\\{1}", Server.MapPath("/Torrents"), torrent.Nome), FileMode.Open))
                {
                    BEncodedDictionary torrentDict = (BEncodedDictionary)BEncodedValue.Decode(torrentStream);
                    BEncodedString trackers = (BEncodedString)torrentDict["announce"];
                    trackers = String.Format("http://tracker.bit-share.net/announce.aspx?passkey={0}", UsuarioLogado.PassKey);
                    torrentDict["announce"] = trackers;
                    buffer = torrentDict.Encode();
                }

                return File(buffer, "application/x-bittorrent", torrent.Nome);
            }
            catch (Exception)
            {
                //TODO: tratar erro
                throw;
            }
        }

       
    }
}
