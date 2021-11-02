using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using GestionEcole1.Models;
namespace GestionEcole1.ConfigurationEntity
{
    public class EtudiantConfiguration:EntityTypeConfiguration<Etudiant>
    {
        public EtudiantConfiguration()
        {
            ToTable("Etudiant");
            Property(e => e.Id)
                .HasColumnName("NumEtudiant");

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
        }
    }

}