using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebReportMWM.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //se debe verificar que si la seccion es null tambien la autentificacion debe estar SignOut
            if (Session["LoggerUser"] == null)
                FormsAuthentication.SignOut();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "WebReport MWM";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Ingenierá MCR";

            return View();
        }
    }
}