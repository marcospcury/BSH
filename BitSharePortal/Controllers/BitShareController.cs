using BitShareData;
using System;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using BitSharePortal.Models;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;

namespace BitSharePortal.Controllers
{
    [Compress]
    public class BitShareController : Controller
    {
        /// <summary>
        /// Data da ultima atualização dos dados do usuário feita a partir da base de dados. 
        /// Para não ficar acessando a base a todo momento, atualiza os dados a cada 5 minutos, ou no momento do primeiro acesso/login
        /// </summary>
        private DateTime DataAtualizacaoDadosUsuario
        {
            get
            {
                return (DateTime)Session["DataAtualizacaoDadosUsuario"];
            }

            set
            {
                Session["DataAtualizacaoDadosUsuario"] = value;
            }
        }

        /// <summary>
        /// Dados do usuário que está logado no momento
        /// </summary>
        public Usuario UsuarioLogado
        {
            get
            {
                // a cada 5 minutos atualiza os dados do usuário, para trazer dados novos de upload, ratio, etc.
                if (Session["UsuarioLogado"] == null || DataAtualizacaoDadosUsuario.AddMinutes(5) <= DateTime.Now)
                {

                        var usuario = Connection.ExecuteQuery<Usuario>(String.Format("select * from usuarios where IdUsuario = {0}", User.Identity.Name)).First(); 
                        Session["UsuarioLogado"] = usuario;
                        DataAtualizacaoDadosUsuario = DateTime.Now;
                   
                }

                return (Usuario)Session["UsuarioLogado"];
            }

            set
            {
                Session["UsuarioLogado"] = value;
                DataAtualizacaoDadosUsuario = DateTime.Now;
            }
        }

        /// <summary>
        /// Pasta física onde estão armazenados os arquivos .torrent
        /// </summary>
        public string PastaTorrents
        {
            get
            { return Server.MapPath("/Torrents"); }
        }

        /// <summary>
        /// Pasta física onde estão armazenados os arquivos de legenda
        /// </summary>
        public string PastaLegendas
        {
            get
            { return Server.MapPath("/Legendas"); }
        }

        /// <summary>
        /// Preenche a viewbag com informações padrão para todas as actions
        /// </summary>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.ActionDescriptor.ActionName != "Login" && filterContext.ActionDescriptor.ActionName != "EnterInvite")
            {
                // preencher a viewbag com coisas comuns a todas as actions
                ViewBag.UsuarioLogado = UsuarioLogado;
                ViewBag.BaseURL = Request.Url.Scheme + "://" + Request.Url.Authority + Request.ApplicationPath.TrimEnd('/');
            }
        }


        protected List<SelectListItem> CarregarListaCategoria()
        {
            var listaRetorno = new List<SelectListItem>();
            listaRetorno.Add(new SelectListItem() { Value = "", Text = "Selecione", Selected = true });
            listaRetorno.Add(new SelectListItem() { Value = "Filmes", Text = "Filmes" });
            listaRetorno.Add(new SelectListItem() { Value = "Séries", Text = "Séries" });
            listaRetorno.Add(new SelectListItem() { Value = "Games", Text = "Games" });
            listaRetorno.Add(new SelectListItem() { Value = "XXX", Text = "XXX" });

            return listaRetorno;
        }

        protected List<SelectListItem> CarregarListaResolucao()
        {
            var listaRetorno = new List<SelectListItem>();
            listaRetorno.Add(new SelectListItem() { Value = "", Text = "Selecione", Selected = true });
            listaRetorno.Add(new SelectListItem() { Value = "640x480", Text = "640x480" });
            listaRetorno.Add(new SelectListItem() { Value = "1280x720", Text = "1280x720" });
            listaRetorno.Add(new SelectListItem() { Value = "1920x1080", Text = "1920x1080" });
            listaRetorno.Add(new SelectListItem() { Value = "Outro", Text = "Outro" });

            return listaRetorno;
        }

        protected List<SelectListItem> CarregarListaAudio()
        {
            var listaRetorno = new List<SelectListItem>();
            listaRetorno.Add(new SelectListItem() { Value = "", Text = "Selecione", Selected = true });
            listaRetorno.Add(new SelectListItem() { Value = "2.0", Text = "2.0" });
            listaRetorno.Add(new SelectListItem() { Value = "2.1", Text = "2.1" });
            listaRetorno.Add(new SelectListItem() { Value = "5.1", Text = "5.1" });
            listaRetorno.Add(new SelectListItem() { Value = "7.1", Text = "7.1" });
            listaRetorno.Add(new SelectListItem() { Value = "7.2", Text = "7.2" });
            listaRetorno.Add(new SelectListItem() { Value = "Outro", Text = "Outro" });

            return listaRetorno;
        }

        protected List<SelectListItem> CarregarListaCodecAudio()
        {
            var listaRetorno = new List<SelectListItem>();
            listaRetorno.Add(new SelectListItem() { Value = "", Text = "Selecione", Selected = true });
            listaRetorno.Add(new SelectListItem() { Value = "MP3(MPG-Layer-3)", Text = "MP3(MPG-Layer-3)" });
            listaRetorno.Add(new SelectListItem() { Value = "AC3", Text = "AC3" });
            listaRetorno.Add(new SelectListItem() { Value = "AAC", Text = "AAC" });
            listaRetorno.Add(new SelectListItem() { Value = "OGG", Text = "OGG" });
            listaRetorno.Add(new SelectListItem() { Value = "DTS", Text = "DTS" });
            listaRetorno.Add(new SelectListItem() { Value = "FLAC", Text = "FLAC" });
            listaRetorno.Add(new SelectListItem() { Value = "MPC", Text = "MPC" });
            listaRetorno.Add(new SelectListItem() { Value = "APE", Text = "APE" });
            listaRetorno.Add(new SelectListItem() { Value = "OFR", Text = "OFR" });
            listaRetorno.Add(new SelectListItem() { Value = "ATRAC3", Text = "ATRAC3" });
            listaRetorno.Add(new SelectListItem() { Value = "RealAudio 7", Text = "RealAudio 7" });
            listaRetorno.Add(new SelectListItem() { Value = "Real Alternative", Text = "Real Alternative" });

            listaRetorno.Add(new SelectListItem() { Value = "Outro", Text = "Outro" });

            return listaRetorno;
        }

        protected List<SelectListItem> CarregarListaCodecVideo()
        {
            var listaRetorno = new List<SelectListItem>();
            listaRetorno.Add(new SelectListItem() { Value = "", Text = "Selecione", Selected = true });

            listaRetorno.Add(new SelectListItem() { Value = "DivX", Text = "DivX" });
            listaRetorno.Add(new SelectListItem() { Value = "RAM (Real Media)", Text = "RAM (Real Media)" });
            listaRetorno.Add(new SelectListItem() { Value = "DAT", Text = "DAT" });
            listaRetorno.Add(new SelectListItem() { Value = "MPEG-1", Text = "MPEG-1" });
            listaRetorno.Add(new SelectListItem() { Value = "MPEG-2", Text = "MPEG-2" });
            listaRetorno.Add(new SelectListItem() { Value = "MPEG-4", Text = "MPEG-4" });
            listaRetorno.Add(new SelectListItem() { Value = "AVC", Text = "AVC" });
            listaRetorno.Add(new SelectListItem() { Value = "H264", Text = "H264" });

            listaRetorno.Add(new SelectListItem() { Value = "Outro", Text = "Outro" });

            return listaRetorno;
        }

        protected string queryBase = "select t.IdTorrent, t.Nome, t.Tamanho, t.Seeds, t.Leechers, t.DataLancamento, 0 as Downloads, u.Nome as UsuarioLancamento From Torrents t Inner Join Usuarios u on u.IdUsuario = t.UsuarioLancamento_IdUsuario"; 
       
    }
}