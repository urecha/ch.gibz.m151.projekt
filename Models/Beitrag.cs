using System;
using System.Collections.Generic;

#nullable disable

namespace ch.gibz.m151.projekt.Models
{
    public partial class Beitrag
    {
        public Beitrag()
        {
            BeitragLikes = new HashSet<BeitragLike>();
            Kommentars = new HashSet<Kommentar>();
        }

        public int Id { get; set; }
        public string Titel { get; set; }
        public DateTime ErstelltAm { get; set; }
        public int AutorId { get; set; }
        public string Inhalt { get; set; }

        public virtual User Autor { get; set; }
        public virtual ICollection<BeitragLike> BeitragLikes { get; set; }
        public virtual ICollection<Kommentar> Kommentars { get; set; }
    }
}
