using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionEcole1.Models
{
    
    public class Matiere
    {
        public Matiere()
        {
            this.Enseignant = new HashSet<Enseignant>();
            this.Notes = new HashSet<Notes>();
        }
        
        public int Id { get; set; }
        public string LibelleCours { get; set; }
        public int SalleId { get; set; }

        public virtual ICollection<Enseignant> Enseignant { get; set; }
        public virtual Salles Salle { get; set; }
        public virtual ICollection<Notes> Notes { get; set; }
        public virtual ICollection<Etudiant> Etudiants { get; set; }
    }
}