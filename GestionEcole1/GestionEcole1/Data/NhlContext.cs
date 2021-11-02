using GestionEcole1.ConfigurationEntity;
using GestionEcole1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GestionEcole1.Data
{
    public class NhlContext: DbContext
    {

        public NhlContext() : base("NhlContext")
        {

        }
        public DbSet<College> Colleges { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Enseignant> Enseignants { get; set; }
        public DbSet<Etudiant> Etudiants { get; set; }
        public DbSet<Matiere> Matieres { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Salles> Salles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CollegeConfiguration());
            modelBuilder.Configurations.Add(new DepartementConfiguration());
            modelBuilder.Configurations.Add(new SalleConfiguration());
            modelBuilder.Configurations.Add(new MatiereConfiguration());
            modelBuilder.Configurations.Add(new NoteConfiguration());
            modelBuilder.Configurations.Add(new EtudiantConfiguration());
            modelBuilder.Configurations.Add(new EnseignantConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
