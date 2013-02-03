using BitShareData;
using BitSharePortal.Models;
using BitSharePortal.Services;
using MonoTorrent.BEncoding;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace BitSharePortal.Controllers
{
    [Authorize]
    public class TorrentsController : BitShareController
    {
        private void SetupListasViewBag()
        {
            ViewBag.ListaCategoria = CarregarListaCategoria();
            ViewBag.ListaResolucao = CarregarListaResolucao();
            ViewBag.ListaAudio = CarregarListaAudio();
            ViewBag.ListaCodecAudio = CarregarListaCodecAudio();
            ViewBag.ListaCodecVideo = CarregarListaCodecVideo();
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
            var repositorio = new DataRepository<BitShareData.Torrent>();
            var torrent = (from item in repositorio.Fetch() where item.IdTorrent == id select item).First();

            return View(torrent);
        }

        [HttpPost]
        public ActionResult Upload(TorrentUploadModel model)
        {
            SetupListasViewBag();

            if (ModelState.IsValid)
            {
                var torrentService = new TorrentServices();
                torrentService.LancarTorrent(Request, model, UsuarioLogado, PastaTorrents, PastaLegendas);

                return View("UploadSucceded");
            }

            ViewBag.EmValidacao = true;
            ViewBag.IdImdb = model.IdImdb;
            return View(model);
        }

        public ActionResult Upload()
        {
            SetupListasViewBag();
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
                var repositorio = new DataRepository<BitShareData.Torrent>();
                var torrent = (from item in repositorio.Fetch() where item.IdTorrent == id select item).First();

                byte[] buffer;
                using (var torrentStream = System.IO.File.Open(String.Format("{0}\\{1}", PastaTorrents, torrent.Arquivo), FileMode.Open))
                {
                    BEncodedDictionary torrentDict = (BEncodedDictionary)BEncodedValue.Decode(torrentStream);
                    BEncodedString trackers = (BEncodedString)torrentDict["announce"];
                    trackers = String.Format("http://tracker.bit-share.net/announce.aspx?passkey={0}", UsuarioLogado.PassKey);
                    torrentDict["announce"] = trackers;
                    buffer = torrentDict.Encode();
                }

                return File(buffer, "application/x-bittorrent", torrent.Arquivo);
            }
            catch (Exception)
            {
                //TODO: tratar erro
                throw;
            }
        }

        public JsonResult ListaTorrentsFilme(string id)
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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult PostComment()
        {
            var servico = new TorrentServices();
            servico.PostarComentario(Request, UsuarioLogado);

            return RedirectToAction("Detail", new { id = Request.Form["IdTorrent"] });
        }
    }
}
