using BitSharePortal.Models;
using MonoTorrent.BEncoding;
using MonoTorrent.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitSharePortal.Controllers
{
    [Authorize]
    public class TorrentsController : BitShareController
    {
        //
        // GET: /Torrents/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Browse(TorrentFilterModel filtro)
        {
            var listaTorrents = new TorrentListModel();
            listaTorrents.CarregarTodosTorrents();
            ViewBag.ListaTorrents = listaTorrents;

            return View(filtro);
        }

        public ActionResult Browse()
        {
            var listaTorrents = new TorrentListModel();
            listaTorrents.CarregarTodosTorrents();
            ViewBag.ListaTorrents = listaTorrents;

            return View();
        }

        public ActionResult Detail(int id)
        {
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        public FileResult Download(int id)
        {
            try
            {
                byte[] buffer;
                using (var torrentStream = System.IO.File.Open("C:\\Users\\Cury\\Desktop\\Torrents To Go\\The Mentalist S05E09 HDTV XviD-SaM.torrent", FileMode.Open))
                {
                    BEncodedDictionary torrentDict = (BEncodedDictionary)BEncodedValue.Decode(torrentStream);
                    BEncodedString trackers = (BEncodedString)torrentDict["announce"];
                    trackers = String.Format("http://tracker.bit-share.net/announce.aspx?passkey={0}", UsuarioLogado.PassKey);
                    torrentDict["announce"] = trackers;
                    buffer = torrentDict.Encode();
                }

                return File(buffer, "application/x-bittorrent", "The Mentalist S05E09 HDTV XviD-SaM.torrent");
            }
            catch (Exception)
            {
                //TODO: tratar erro
                throw;
            }
        }
    }
}
