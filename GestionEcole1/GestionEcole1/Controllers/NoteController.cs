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
    public class NoteController : Controller
    {
        private NhlContext db = new NhlContext();

        // GET: Note
        public ActionResult Index()
        {
            var notes = db.Notes.Include(n => n.Etudiant).Include(n => n.Matiere);
            return View(notes.ToList());
        }


        public ActionResult exportreport(int id)
        {
            Matiere matiere = db.Matieres.Find(id);
            if (matiere == null)
            {
                return HttpNotFound();
            }
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "CrMatMoy.rpt"));
            rd.SetDataSource(db.Notes.Select(n => new
            {
                LibelleCours=n.Matiere.LibelleCours,
                NumMatiere=n.Matiere.Id,
                EtudiantNom=n.Etudiant.Nom,
                Etudiantprenom = n.Etudiant.Prenom,
                noteControle=n.NoteControle,
                NumEtudiant=n.EtudiantId
               
            }).Where(e => e.NumMatiere == id).ToList());
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "MatiereMoyenne.pdf");
            }
            catch
            {
                throw;
            }
        }
        // GET: Note/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notes notes = db.Notes.Find(id);
            if (notes == null)
            {
                return HttpNotFound();
            }
            return View(notes);
        }

        // GET: Note/Create
        public ActionResult Create()
        {
            ViewBag.EtudiantId = new SelectList(db.Etudiants, "Id", "Nom");
            ViewBag.MatiereId = new SelectList(db.Matieres, "Id", "LibelleCours");
            return View();
        }

        // POST: Note/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NoteControle,MatiereId,EtudiantId")] Notes notes)
        {
            if (ModelState.IsValid)
            {
                db.Notes.Add(notes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EtudiantId = new SelectList(db.Etudiants, "Id", "Nom", notes.EtudiantId);
            ViewBag.MatiereId = new SelectList(db.Matieres, "Id", "LibelleCours", notes.MatiereId);
            return View(notes);
        }

        // GET: Note/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notes notes = db.Notes.Find(id);
            if (notes == null)
            {
                return HttpNotFound();
            }
            ViewBag.EtudiantId = new SelectList(db.Etudiants, "Id", "Nom", notes.EtudiantId);
            ViewBag.MatiereId = new SelectList(db.Matieres, "Id", "LibelleCours", notes.MatiereId);
            return View(notes);
        }

        // POST: Note/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NoteControle,MatiereId,EtudiantId")] Notes notes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EtudiantId = new SelectList(db.Etudiants, "Id", "Nom", notes.EtudiantId);
            ViewBag.MatiereId = new SelectList(db.Matieres, "Id", "LibelleCours", notes.MatiereId);
            return View(notes);
        }

        // GET: Note/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notes notes = db.Notes.Find(id);
            if (notes == null)
            {
                return HttpNotFound();
            }
            return View(notes);
        }

        // POST: Note/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notes notes = db.Notes.Find(id);
            db.Notes.Remove(notes);
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
