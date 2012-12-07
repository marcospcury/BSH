using BitShareData;
using BitSharePortal.Models;
using System;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web.Security;
using System.Linq;

namespace BitSharePortal.Controllers
{
    public class UsersController : BitShareController
    {
        //
        // GET: /Users/

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login(string userName, string password, string returnUrl)
        {
            return View();
        }

        public ActionResult EnterInvite()
        {
            return View();
        }

        [Authorize]
        public ActionResult Profile(int id)
        {
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Login(LoginModel login, string returnUrl)
        {
            bool usuarioValido = false;
           
            try
            {
                var usuario = Connection.ExecuteQuery<Usuario>(String.Format("select * from usuarios where Nome = '{0}' And Senha = '{1}'", login.Username, login.Password)).First();
                usuarioValido = true;
                UsuarioLogado = usuario;
            }
            catch (Exception)
            {
                // qualquer falha considera usuário inválido
            }

            if (usuarioValido)
            {
                FormsAuthentication.SetAuthCookie(UsuarioLogado.IdUsuario.ToString(), login.Connected);
                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                       && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "");
                return View(login);
            }
        }

    }
}
