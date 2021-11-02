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
    public class EnseignantController : Controller
    {
        private NhlContext db = new NhlContext();

        // GET: Enseignant
        public ActionResult Index()
        {
            var enseignants = db.Enseignants.Include(e => e.Departement).Include(e => e.Matiere);
            return View(enseignants.ToList());
        }

        public ActionResult exportreport(int id)
        {
            Enseignant enseignant = db.Enseignants.Find(id);
            if (enseignant == null)
            {
                return HttpNotFound();
            }
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "CrEnseignant.rpt"));
            rd.SetDataSource(db.Enseignants.Select(e => new
            {

                nom = e.Nom,
                prenom = e.Prenom,
                mail = e.Mail,
                tel = e.Tel,
                indice = e.Indice,
                NumEnseignant=e.Id
            }).Where(e => e.NumEnseignant==id).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Enseignant.pdf");
            }
            catch
            {
                throw;
            }
        }

        public ActionResult exportreport2(int id)
        {
            Matiere matiere = db.Matieres.Find(id);
            if (matiere == null)
            {
                return HttpNotFound();
            }
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "CrMatEns.rpt"));
            rd.SetDataSource(db.Enseignants.Select(e => new
            {
                LibelleCours = e.Matiere.LibelleCours,
                NomEnseignant = e.Nom,
                Prenom = e.Prenom,
                num= e.MatiereId

            }).Where(e=> e.num==id).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "MatiereEnseignant.pdf");
            }
            catch
            {
                throw;
            }
        }

        // GET: Enseignant/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enseignant enseignant = db.Enseignants.Find(id);
            if (enseignant == null)
            {
                return HttpNotFound();
            }
            return View(enseignant);
        }

        // GET: Enseignant/Create
        public ActionResult Create()
        {
            ViewBag.DepartementId = new SelectList(db.Departements, "Id", "Nom");
            ViewBag.MatiereId = new SelectList(db.Matieres, "Id", "LibelleCours");
            return View();
        }

        // POST: Enseignant/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom,Prenom,Tel,Mail,DatePriseDeFonction,Indice,MatiereId,DepartementId")] Enseignant enseignant)
        {
            if (ModelState.IsValid)
            {
                db.Enseignants.Add(enseignant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartementId = new SelectList(db.Departements, "Id", "Nom", enseignant.DepartementId);
            ViewBag.MatiereId = new SelectList(db.Matieres, "Id", "LibelleCours", enseignant.MatiereId);
            return View(enseignant);
        }

        // GET: Enseignant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enseignant enseignant = db.Enseignants.Find(id);
            if (enseignant == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartementId = new SelectList(db.Departements, "Id", "Nom", enseignant.DepartementId);
            ViewBag.MatiereId = new SelectList(db.Matieres, "Id", "LibelleCours", enseignant.MatiereId);
            return View(enseignant);
        }

        // POST: Enseignant/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom,Prenom,Tel,Mail,DatePriseDeFonction,Indice,MatiereId,DepartementId")] Enseignant enseignant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enseignant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartementId = new SelectList(db.Departements, "Id", "Nom", enseignant.DepartementId);
            ViewBag.MatiereId = new SelectList(db.Matieres, "Id", "LibelleCours", enseignant.MatiereId);
            return View(enseignant);
        }

        // GET: Enseignant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enseignant enseignant = db.Enseignants.Find(id);
            if (enseignant == null)
            {
                return HttpNotFound();
            }
            return View(enseignant);
        }

        // POST: Enseignant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enseignant enseignant = db.Enseignants.Find(id);
            db.Enseignants.Remove(enseignant);
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
