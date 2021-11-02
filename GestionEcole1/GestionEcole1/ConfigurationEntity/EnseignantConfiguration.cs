using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using GestionEcole1.Models;

namespace GestionEcole1.ConfigurationEntity
{
    public class EnseignantConfiguration:EntityTypeConfiguration<Enseignant>
    {
        public EnseignantConfiguration()
        {
            ToTable("Enseignant");
            Property(e => e.Id)
          .HasColumnName("NumEnseignant");
            Property(e => e.Nom)
          .IsRequired()
          .HasMaxLength(50)
          .HasColumnType("Varchar");

            Property(e => e.Prenom)
          .IsRequired()
          .HasMaxLength(50)
          .HasColumnType("Varchar");

            Property(e => e.Mail)
          .IsRequired()
          .HasMaxLength(100)
          .HasColumnType("Varchar");

            Property(e => e.DatePriseDeFonction)
          .IsRequired();
          

            //enseigner
            HasRequired(t => t.Matiere)
                .WithMany(e => e.Enseignant)
                .HasForeignKey(t => t.MatiereId)
                .WillCascadeOnDelete(false);

            //diriger
            HasOptional(e => e.Departement)
                .WithRequired(d => d.EnseignantChef);

            //travailler
            HasRequired(e => e.Departement)
                .WithMany(d => d.Enseignant);


        }
    }
}