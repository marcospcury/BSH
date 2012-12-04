using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BitSharePortal.Controllers
{
    [Authorize]
    public class HomeController : BitShareController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Regras()
        {
            return View();
        }

        public ActionResult Contato()
        {
            return View();
        }
    }
}
