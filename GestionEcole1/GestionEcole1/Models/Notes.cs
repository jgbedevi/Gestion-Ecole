using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GestionEcole1.Models
{
    
    public class Notes
    {
        public int Id { get; set; }
        public double NoteControle { get; set; }
        public int MatiereId { get; set; }
        public int EtudiantId { get; set; }

        public virtual Etudiant Etudiant { get; set; }
        public virtual Matiere Matiere { get; set; }
        public object LibelleCours { get; internal set; }
    }
}