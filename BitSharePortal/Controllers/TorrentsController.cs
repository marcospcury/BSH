using BitShareData;
using BitSharePortal.Models;
using MonoTorrent.BEncoding;
using MonoTorrent.Common;
using System;
using System.Collections.Generic;
using System.Data.Objects;
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
        public ActionResult Upload(TorrentUploadModel model)
        {
            ViewBag.ListaCategoria = CarregarListaCategoria();
            ViewBag.ListaResolucao = CarregarListaResolucao();
            ViewBag.ListaAudio = CarregarListaAudio();
            ViewBag.ListaCodecAudio = CarregarListaCodecAudio();
            ViewBag.ListaCodecVideo = CarregarListaCodecVideo();

            HttpPostedFileBase torrentFile = Request.Files["torrentFile"];

            if (ModelState.IsValid)
            {
                try
                {
                    var arquivoTorrent = String.Format("{0}\\{1}", Server.MapPath("/Torrents"), torrentFile.FileName);
                    torrentFile.SaveAs(arquivoTorrent);
                    var dadosTorrent = MonoTorrent.Common.Torrent.Load(arquivoTorrent);

                    Usuario usuarioLancamento;

                    var repositorioPrincipal = new DataRepository<BitShareData.Torrent>();
                    var bitShareTorrent = new BitShareData.Torrent();

                    var filmeImdb = CarregarFilmeImdb(model.IdImdb);

                    var repositorioFilme = new DataRepository<Filme>(repositorioPrincipal.Context);
                    var filmesEncontrados = repositorioFilme.Find(f => f.IdImdb == model.IdImdb);
                    if (filmesEncontrados.Count() > 0)
                    {
                        bitShareTorrent.Filme = filmesEncontrados.First();
                    }
                    else
                    {
                        bitShareTorrent.Filme = GravarNovoFilme(repositorioFilme, filmeImdb, model);
                    }

                    var nomeTorrent = String.Format("[{0}] [{1} {2}] [{3} {4}]", filmeImdb.Titulo, model.CodecVideo, model.Resolucao, model.CodecAudio, model.Audio);

                    if (model.Dublado)
                    {
                        nomeTorrent += " [Dub]";
                    }

                    bitShareTorrent.IdTorrent = 0;
                    bitShareTorrent.Nome = nomeTorrent;
                    bitShareTorrent.Arquivo = torrentFile.FileName;
                    bitShareTorrent.Categoria = "";//model.Categoria;
                    bitShareTorrent.HashInfo = dadosTorrent.InfoHash.ToString();
                    bitShareTorrent.Tamanho = dadosTorrent.Size;
                    bitShareTorrent.Seeds = 0;
                    bitShareTorrent.Leechers = 0;
                    bitShareTorrent.DataLancamento = DateTime.Now;
                    bitShareTorrent.FreeLeech = false;
                    bitShareTorrent.Ativo = true;
                    bitShareTorrent.PrimeiroSnatch = false;
                    bitShareTorrent.Downloads = 0;
                    bitShareTorrent.Resolucao = model.Resolucao;
                    bitShareTorrent.Audio = model.Audio;
                    bitShareTorrent.CodecAudio = model.CodecAudio;
                    bitShareTorrent.CodecVideo = model.CodecVideo;
                    bitShareTorrent.Observacoes = model.Observacoes == null ? "" : model.Observacoes;
                    bitShareTorrent.Dublado = model.Dublado;

                    if (Request.Files.Count > 1)
                    {
                        for (int indice = 1; indice < Request.Files.Count; indice++)
                        {
                            if (Request.Files[indice].FileName != "")
                            {
                                Request.Files[indice].SaveAs(String.Format("{0}\\{1}", Server.MapPath("/Legendas"), Request.Files[indice].FileName));
                                var legenda = new Legenda();
                                legenda.IdLegenda = 0;
                                legenda.Idioma = model.idiomaLegenda[indice - 1];
                                legenda.Arquivo = Request.Files[indice].FileName;
                                bitShareTorrent.Filme.Legendas.Add(legenda);
                            }
                        }
                    }

                    foreach (var file in dadosTorrent.Files)
                    {
                        var arquivo = new ArquivoTorrent();
                        arquivo.Nome = file.Path;
                        arquivo.Tamanho = file.Length;

                        bitShareTorrent.ArquivoTorrents.Add(arquivo);
                    }

                    // recupera o usuário pela entity, para poder fazer o relacionamento com o Torrent
                    var repositorioUsuario = new DataRepository<BitShareData.Usuario>(repositorioPrincipal.Context);
                    usuarioLancamento = repositorioUsuario.First(u => u.IdUsuario == UsuarioLogado.IdUsuario);

                    bitShareTorrent.UsuarioLancamento = usuarioLancamento;

                    repositorioPrincipal.Add(bitShareTorrent);
                    repositorioPrincipal.SaveChanges();

                    return View("UploadSucceded");    
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    // dispose do repositorio
                }
            }
            else
            {
                ViewBag.EmValidacao = true;
                ViewBag.IdImdb = model.IdImdb;
                return View(model);
            }
        }

        private Filme GravarNovoFilme(DataRepository<Filme> repositorioFilme, FilmeIMDBJsonResult filmeImdb, TorrentUploadModel model)
        {
            var filme = new Filme();
            filme.IdFilme = 0;
            filme.IdImdb = filmeImdb.IDImdb;
            filme.Nome = filmeImdb.Titulo;
            filme.Diretor = filmeImdb.Diretor;
            filme.AnoLancamento = filmeImdb.AnoLancamento;
            filme.Sinopse = filmeImdb.Sinopse;
            filme.URLPoster = filmeImdb.URLPoster;
            filme.Generos = filmeImdb.Generos;
            filme.TrailerYoutube = model.TrailerYoutube == null ? "" : model.TrailerYoutube;
            filme.ScreenShots = ""; //TODO

            foreach (var atorImdb in filmeImdb.ListaAtores)
            {
                var repositorioAtor = new DataRepository<Ator>(repositorioFilme.Context);
                var atores = repositorioAtor.Find(a => a.IdImdb == atorImdb.IDImdb);
                Ator atorAtual;
                if (atores.Count() > 0)
                {
                    atorAtual = atores.First();
                }
                else
                {
                    atorAtual = new Ator();
                    atorAtual.IdAtor = 0;
                    atorAtual.Nome = atorImdb.Nome;
                    atorAtual.IdImdb = atorImdb.IDImdb;
                    atorAtual.URLFoto = atorImdb.URLFoto == null ? "" : atorImdb.URLFoto;
                    repositorioAtor.Add(atorAtual);
                }

                filme.Papels.Add(new Papel() { Ator = atorAtual, IdPapel = 0, NomePersonagem = atorImdb.Papel });
            }

            return filme;
        }

        public ActionResult ListaTorrentsFilme(string id)
        {
            string retorno = "";
            var filtro = String.Format(" where Filme_IdFilme in (select IdFilme From Filmes where IdImdb = '{0}')", id);
            var listaTorrents = Connection.ExecuteQuery<TorrentModel>(queryBase + filtro);

            if (listaTorrents.Count() > 0)
            {
                retorno += "<table width='100%'>";
                foreach (var torrent in listaTorrents)
                {
                    retorno += String.Format("<tr><td width='470'><a href='Detail/{0}'>{1}</a></td><td>{2}</td><td>{3}</td></tr>", torrent.IdTorrent, torrent.Nome, torrent.DataLancamento.ToString("dd/MM/yyyy hh:mm"), torrent.TamanhoTorrent);
                }
                retorno += "</table>";
            }

            return Json(new { tabela = retorno }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Upload()
        {
            ViewBag.ListaCategoria = CarregarListaCategoria();
            ViewBag.ListaResolucao = CarregarListaResolucao();
            ViewBag.ListaAudio = CarregarListaAudio();
            ViewBag.ListaCodecAudio = CarregarListaCodecAudio();
            ViewBag.ListaCodecVideo = CarregarListaCodecVideo();
            ViewBag.EmValidacao = false;

            return View();
        }

        public ActionResult UploadSucceded()
        {
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
