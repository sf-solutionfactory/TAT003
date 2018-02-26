using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WF002.Entities;

namespace WF002.Controllers
{
    public class PaginaController : Controller
    {
        private TAT001Entities db = new TAT001Entities();

        // GET: PAGINATs
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
            var pAGINATs = db.PAGINATs.Include(p => p.PAGINA).Include(p => p.SPRA);
            return View(pAGINATs.ToList());
        }

        // GET: PAGINATs/Details/5
        public ActionResult Details(string id)
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGINAT pAGINAT = db.PAGINATs.Find(id);
            if (pAGINAT == null)
            {
                return HttpNotFound();
            }
            return View(pAGINAT);
        }

        // GET: PAGINATs/Create
        public ActionResult Create()
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
            ViewBag.ID = new SelectList(db.PAGINAs, "ID", "URL");
            ViewBag.SPRAS_ID = new SelectList(db.SPRAS, "ID", "DESCRIPCION");
            return View();
        }

        // POST: PAGINATs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SPRAS_ID,ID,TXT50")] PAGINAT pAGINAT)
        {
            if (ModelState.IsValid)
            {
                db.PAGINATs.Add(pAGINAT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID = new SelectList(db.PAGINAs, "ID", "URL", pAGINAT.ID);
            ViewBag.SPRAS_ID = new SelectList(db.SPRAS, "ID", "DESCRIPCION", pAGINAT.SPRAS_ID);
            return View(pAGINAT);
        }

        // GET: PAGINATs/Edit/5
        public ActionResult Edit(string id)
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGINAT pAGINAT = db.PAGINATs.Find(id);
            if (pAGINAT == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.PAGINAs, "ID", "URL", pAGINAT.ID);
            ViewBag.SPRAS_ID = new SelectList(db.SPRAS, "ID", "DESCRIPCION", pAGINAT.SPRAS_ID);
            return View(pAGINAT);
        }

        // POST: PAGINATs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SPRAS_ID,ID,TXT50")] PAGINAT pAGINAT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pAGINAT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.PAGINAs, "ID", "URL", pAGINAT.ID);
            ViewBag.SPRAS_ID = new SelectList(db.SPRAS, "ID", "DESCRIPCION", pAGINAT.SPRAS_ID);
            return View(pAGINAT);
        }

        // GET: PAGINATs/Delete/5
        public ActionResult Delete(string id)
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGINAT pAGINAT = db.PAGINATs.Find(id);
            if (pAGINAT == null)
            {
                return HttpNotFound();
            }
            return View(pAGINAT);
        }

        // POST: PAGINATs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            PAGINAT pAGINAT = db.PAGINATs.Find(id);
            db.PAGINATs.Remove(pAGINAT);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
