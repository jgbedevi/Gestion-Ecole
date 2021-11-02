using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionEcole1.Data;
using GestionEcole1.Models;

namespace GestionEcole1.Controllers
{
    public class SalleController : Controller
    {
        private NhlContext db = new NhlContext();

        // GET: Salle
        public ActionResult Index()
        {
            return View(db.Salles.ToList());
        }

        // GET: Salle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salles salles = db.Salles.Find(id);
            if (salles == null)
            {
                return HttpNotFound();
            }
            return View(salles);
        }

        // GET: Salle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Salle/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom,Nb_Place")] Salles salles)
        {
            if (ModelState.IsValid)
            {
                db.Salles.Add(salles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salles);
        }

        // GET: Salle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salles salles = db.Salles.Find(id);
            if (salles == null)
            {
                return HttpNotFound();
            }
            return View(salles);
        }

        // POST: Salle/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,Nb_Place")] Salles salles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salles);
        }

        // GET: Salle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salles salles = db.Salles.Find(id);
            if (salles == null)
            {
                return HttpNotFound();
            }
            return View(salles);
        }

        // POST: Salle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salles salles = db.Salles.Find(id);
            db.Salles.Remove(salles);
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
