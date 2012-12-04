using BitShareData;
using BitSharePortal.Models;
using System;
using System.Web.Mvc;
using System.Web.Security;

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
                using (var repositorio = new DataRepository<Usuario>())
                {
                    var usuario = repositorio.Single(u => u.Nome == login.Username && u.Senha == login.Password);
                    UsuarioLogado = usuario;

                    usuarioValido = true;
                }
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
