using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionEcole1.Models;
using System.Data.Entity.ModelConfiguration;
namespace GestionEcole1.ConfigurationEntity
{
    public class DepartementConfiguration:EntityTypeConfiguration<Departement>
    {
        public DepartementConfiguration()
        {
            ToTable("Departement");

            Property(d => d.Id)
                .HasColumnName("Code_Departement");

            Property(e => e.Nom)
         .IsRequired()
         .HasMaxLength(50)
         .HasColumnType("Varchar");

        
        }
    }
}