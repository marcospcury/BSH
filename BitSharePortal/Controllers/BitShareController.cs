﻿using BitShareData;
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
            listaRetorno.Add(new SelectListItem() { Value = "480", Text = "480" });
            listaRetorno.Add(new SelectListItem() { Value = "720", Text = "720" });
            listaRetorno.Add(new SelectListItem() { Value = "1080", Text = "1080" });
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

        /// <summary>
        /// Carrega um filme via serviço REST
        /// </summary>
        /// <param name="id">ID do Imdb do filme</param>
        protected FilmeIMDBJsonResult CarregarFilmeImdb(string id)
        {
            var filme = new FilmeIMDBJsonResult();

            try
            {
                var request = WebRequest.Create(String.Format("http://www.bit-share-rest.net/Movies/GetComplete/?id={0}&JsonCallback=callback", id)) as HttpWebRequest;
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format(
                        "Server error (HTTP {0}: {1}).",
                        response.StatusCode,
                        response.StatusDescription));
                    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
                    using (var streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        var responseText = streamReader.ReadToEnd();
                        filme = jsonSerializer.Deserialize<FilmeIMDBJsonResult>(responseText.Replace("callback(", "").Replace(");", ""));
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return filme;
        }
    }
}