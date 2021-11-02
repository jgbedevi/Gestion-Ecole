using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using GestionEcole1.Models;

namespace GestionEcole1.ConfigurationEntity
{
    public class MatiereConfiguration:EntityTypeConfiguration<Matiere>
    {
        public MatiereConfiguration()
        {
            ToTable("Matiere");
            Property(m => m.Id)
                .HasColumnName("NumMatiere");

            Property(e => e.LibelleCours)
          .IsRequired()
          .HasMaxLength(50)
          .HasColumnType("Varchar");

            HasMany(m => m.Enseignant)
                .WithRequired(e => e.Matiere)
                .HasForeignKey(e => e.MatiereId);
        }
    }
}