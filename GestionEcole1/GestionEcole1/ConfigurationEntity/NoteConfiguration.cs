using GestionEcole1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace GestionEcole1.ConfigurationEntity
{
    public class NoteConfiguration:EntityTypeConfiguration<Notes>
    {
        public NoteConfiguration()
        {
            Property(n => n.Id)
                .HasColumnName("NumNote");
            Property(n => n.NoteControle)
                .IsRequired();
            HasRequired(n => n.Matiere)
                .WithMany(e => e.Notes)
                .HasForeignKey(e=>e.MatiereId)
                .WillCascadeOnDelete(false);

            HasRequired(n => n.Etudiant)
                .WithMany(e => e.Notes)
                .HasForeignKey(e => e.EtudiantId)
                .WillCascadeOnDelete(false);
            Map(n => n.ToTable("Notes"));

        }
    }
}