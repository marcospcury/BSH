using BitShareData;
using System;
using System.Web.Mvc;

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
                    using (var repositorio = new DataRepository<Usuario>())
                    {
                        var usuario = repositorio.Single(u => u.IdUsuario == Int32.Parse(User.Identity.Name));
                        Session["UsuarioLogado"] = usuario;
                        DataAtualizacaoDadosUsuario = DateTime.Now;
                    }
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
    }
}