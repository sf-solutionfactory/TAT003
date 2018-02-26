using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WF002.Entities;
using WF002.Models;

namespace WF002.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            using (TAT001Entities db = new TAT001Entities())
            {
                string u = Session["UserID"].ToString();
                var user = db.USUARIOs.Where(a => a.ID.Equals(u)).FirstOrDefault();
                var obj = db.PAGINAVs.Where(a => a.ID.Equals(user.ID)).ToList();
                if (obj != null)
                    ViewBag.permisos = obj;
                var obj2 = db.CARPETAVs.Where(a => a.USUARIO_ID.Equals(user.ID)).ToList();
                if (obj2 != null)
                    ViewBag.carpetas = obj2;
                ViewBag.nombre = user.NOMBRE + " " + user.APELLIDO_P + " " + user.APELLIDO_M;
                ViewBag.email = user.EMAIL;
                ViewBag.rol = user.MIEMBROS.FirstOrDefault().ROL.NOMBRE;
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(USUARIO objUser, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                if (objUser.PASS != null)
                {
                    using (TAT001Entities db = new TAT001Entities())
                    {
                        Cryptography c = new Cryptography();
                        string pass = c.Encrypt(objUser.PASS);

                        var obj = db.USUARIOs.Where(a => a.ID.Equals(objUser.ID) && a.PASS.Equals(pass)).FirstOrDefault();
                        if (obj != null)
                        {
                            Session["UserID"] = obj.ID.ToString();
                            Session["UserName"] = obj.NOMBRE.ToString();
                            FormsAuthentication.SetAuthCookie(obj.ID.ToString(), false);
                            if (ReturnUrl == null)
                                return RedirectToAction("Index", "Home");
                            if (ReturnUrl.Equals("/"))
                                return RedirectToAction("Index", "Home");
                            string[] ret = ReturnUrl.Split('/');
                            FormsAuthentication.RedirectFromLoginPage(obj.ID.ToString(), true);
                            return RedirectToAction(ret[ret.Length - 1], ret[ret.Length - 2]);
                        }
                        else
                            ViewBag.Message = "Usuario o contraseña incorrecta";
                    }
                }
            }
            return View(objUser);
        }
    }
}