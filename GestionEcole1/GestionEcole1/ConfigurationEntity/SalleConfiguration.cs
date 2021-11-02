using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.ModelConfiguration;
using GestionEcole1.Models;

namespace GestionEcole1.ConfigurationEntity
{
    public class SalleConfiguration : EntityTypeConfiguration<Salles>
    {
        public SalleConfiguration()
        {
            ToTable("Salle");
            Property(s => s.Id)
                .HasColumnName("No_Salle");
            Property(e => e.Nom)
          .IsRequired()
          .HasColumnName("NomSalle")
          .HasMaxLength(50)
          .HasColumnType("Varchar");
            Property(e => e.Nb_Place)
          .IsRequired();
          

            HasMany(s => s.Matiere)
                .WithRequired(m => m.Salle)
                .HasForeignKey(m => m.SalleId);
        }
        
    }
}