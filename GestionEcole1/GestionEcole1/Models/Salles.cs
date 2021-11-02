using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionEcole1.Models
{
    
    public class Salles
    {
        public Salles()
        {
            this.Matiere = new HashSet<Matiere>();
        }
        
        public int Id { get; set; }
        public string Nom { get; set; }
        public int Nb_Place { get; set; }


        public virtual ICollection<Matiere> Matiere { get; set; }

    }
}