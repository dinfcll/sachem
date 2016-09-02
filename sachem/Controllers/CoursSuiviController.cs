using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using sachem.Models;

namespace sachem.Controllers
{
    public class CoursSuiviController : Controller
    {
        private SACHEMEntities db = new SACHEMEntities();

        // GET: CoursSuivi
        public ActionResult Index()
        {
            return View(db.CoursSuivi.ToList());
        }

        // GET: CoursSuivi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursSuivi coursSuivi = db.CoursSuivi.Find(id);
            if (coursSuivi == null)
            {
                return HttpNotFound();
            }
            return View(coursSuivi);
        }

        // GET: CoursSuivi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CoursSuivi/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_CoursReussi,id_Sess,id_Pers,id_College,id_Statut,id_Cours,resultat,autre_Cours,autre_College")] CoursSuivi coursSuivi)
        {
            if (ModelState.IsValid)
            {
                db.CoursSuivi.Add(coursSuivi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(coursSuivi);
        }

        // GET: CoursSuivi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursSuivi coursSuivi = db.CoursSuivi.Find(id);
            if (coursSuivi == null)
            {
                return HttpNotFound();
            }
            return View(coursSuivi);
        }

        // POST: CoursSuivi/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_CoursReussi,id_Sess,id_Pers,id_College,id_Statut,id_Cours,resultat,autre_Cours,autre_College")] CoursSuivi coursSuivi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coursSuivi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coursSuivi);
        }

        // GET: CoursSuivi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoursSuivi coursSuivi = db.CoursSuivi.Find(id);
            if (coursSuivi == null)
            {
                return HttpNotFound();
            }
            return View(coursSuivi);
        }

        // POST: CoursSuivi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CoursSuivi coursSuivi = db.CoursSuivi.Find(id);
            db.CoursSuivi.Remove(coursSuivi);
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
