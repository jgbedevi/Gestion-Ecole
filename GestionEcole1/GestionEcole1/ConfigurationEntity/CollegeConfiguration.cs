using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using GestionEcole1.Models;

namespace GestionEcole1.ConfigurationEntity
{
    public class CollegeConfiguration:EntityTypeConfiguration<College>
    {
        public CollegeConfiguration()
        {
            ToTable("College");
            Property(c => c.Id)
                .HasColumnName("CodeCollege");

            Property(c => c.Nom)
                .IsRequired()
                .HasColumnType("Varchar")
                .HasMaxLength(100);

            Property(e => e.Adresse)
          .IsRequired()
          .HasMaxLength(50)
          .HasColumnType("Varchar");

            HasMany(c => c.Departement)
                .WithRequired(d => d.College)
                .HasForeignKey(d => d.CollegeId);

        }
    }
}