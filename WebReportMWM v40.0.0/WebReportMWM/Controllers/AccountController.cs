using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebReportMWM.Models.Entitys;
using WebReportMWM.services;

namespace WebReportMWM.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserValidation(string userName, string userPassword)
        {
            TempData["MessageStatus"] = "";
            using (var context = new DMMeatWeigherModel())
            {
                Session["LoggerUser"] = context.operadores.Where(x => x.Nombre == userName && x.pasw == userPassword).SingleOrDefault();
                if(Session["LoggerUser"]!= null)
                {
                    FormsAuthentication.SetAuthCookie(((Operadores)Session["LoggerUser"]).Nombre, false);
                    return RedirectToAction("Index", "Home");
                }
                FormsAuthentication.SignOut();
                TempData["MessageStatus"] = "El nombre de Usuario o Password no son validos !!!";
                return View("UserLogin");
            } 
        }

        public ActionResult UserOut()
        {
            FormsAuthentication.SignOut();
            Session["LoggerUser"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}