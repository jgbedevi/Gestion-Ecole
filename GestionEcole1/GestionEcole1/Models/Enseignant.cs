using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionEcole1.Models
{
    [Table("Enseignant")]

    public class Enseignant
    {
       /* public Enseignant()
        {
            //this.Departement = new HashSet<Departement>();
        }*/
        
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int Tel { get; set; }
        public string Mail { get; set; }
        public DateTime DatePriseDeFonction { get; set; }
        public int Indice { get; set; }
        public int MatiereId { get; set; }

        //public virtual ICollection<Departement> Departement { get; set; }
        public virtual Departement Departement { get; set; }
        public int DepartementId { get; set; }
        public virtual Matiere Matiere { get; set; }
    }
}