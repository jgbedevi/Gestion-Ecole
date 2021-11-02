using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionEcole1.Models
{
    
    public class Etudiant
    {
        public Etudiant()
        {
            this.Notes = new HashSet<Notes>();
        }
        
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Tel { get; set; }
        public string Mail { get; set; }
        public DateTime AnneEntree { get; set; }
        //public virtual Departement Departement { get; set; }
        //public int DepartementId { get; set; }
        public virtual ICollection<Notes> Notes { get; set; }
        public virtual ICollection<Matiere> Matieres { get; set; }
    }

    }