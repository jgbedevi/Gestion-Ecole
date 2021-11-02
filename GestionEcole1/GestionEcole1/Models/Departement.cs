using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionEcole1.Models
{
    [Table("Departement")]
    public class Departement
    {
        public Departement()
        {
            this.Enseignant = new HashSet<Enseignant>();
        }
        
        public int Id { get; set; }
        public string Nom { get; set; }
        public int CollegeId { get; set; }
        public virtual College College { get; set; }
        public virtual Enseignant EnseignantChef { get; set; }
        public virtual ICollection<Enseignant> Enseignant { get; set; }
    }
}