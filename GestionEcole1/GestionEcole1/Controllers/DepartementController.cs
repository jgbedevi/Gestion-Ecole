using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using GestionEcole1.Data;
using GestionEcole1.Models;

namespace GestionEcole1.Controllers
{
    public class DepartementController : Controller
    {
        private NhlContext db = new NhlContext();

        // GET: Departement
        public ActionResult Index()
        {
            var departements = db.Departements.Include(d => d.College).Include(d=>d.EnseignantChef);
            return View(departements.ToList());
        }
         public ActionResult exportreport()
         {

             ReportDocument rd = new ReportDocument();
             rd.Load(Path.Combine(Server.MapPath("~/Report"), "DepReport2.rpt"));
             rd.SetDataSource(db.Departements.Select(e => new
             {
                 college=e.College.Nom,
                 Departement=e.Nom,
                 DirecteurNom=e.EnseignantChef.Nom,
                 prenom = e.EnseignantChef.Prenom
             }).ToList());
             Response.Buffer = false;
             Response.ClearContent();
             Response.ClearHeaders();
             try
             {
                 Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                 stream.Seek(0, SeekOrigin.Begin);
                 return File(stream, "application/pdf", "CollegeDepartement.pdf");
             }
             catch
             {
                 throw;
             }
         }

        // GET: Departement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departement departement = db.Departements.Find(id);
            if (departement == null)
            {
                return HttpNotFound();
            }
            return View(departement);
        }

        // GET: Departement/Create
        public ActionResult Create()
        {
            ViewBag.CollegeId = new SelectList(db.Colleges, "Id", "Nom");
            return View();
        }

        // POST: Departement/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom,CollegeId")] Departement departement)
        {
            if (ModelState.IsValid)
            {
                db.Departements.Add(departement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CollegeId = new SelectList(db.Colleges, "Id", "Nom", departement.CollegeId);
            return View(departement);
        }

        // GET: Departement/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departement departement = db.Departements.Find(id);
            if (departement == null)
            {
                return HttpNotFound();
            }
            ViewBag.CollegeId = new SelectList(db.Colleges, "Id", "Nom", departement.CollegeId);
            return View(departement);
        }

        // POST: Departement/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,CollegeId")] Departement departement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CollegeId = new SelectList(db.Colleges, "Id", "Nom", departement.CollegeId);
            return View(departement);
        }

        // GET: Departement/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departement departement = db.Departements.Find(id);
            if (departement == null)
            {
                return HttpNotFound();
            }
            return View(departement);
        }

        // POST: Departement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Departement departement = db.Departements.Find(id);
            db.Departements.Remove(departement);
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
