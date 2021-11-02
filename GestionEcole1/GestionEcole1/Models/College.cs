using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionEcole1.Models
{
    

    public class College
    {

        public College()
        {
            this.Departement = new HashSet<Departement>();
        }
        
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }

        public virtual ICollection<Departement> Departement { get; set; }

    }
}