using System;
using System.Collections.Generic;

#nullable disable

namespace ch.gibz.m151.projekt.Models
{
    public partial class Kommentar
    {
        public Kommentar()
        {
            KommentarLikes = new HashSet<KommentarLike>();
        }

        public int Id { get; set; }
        public int AutorId { get; set; }
        public int BeitragId { get; set; }
        public string Titel { get; set; }
        public string Inhalt { get; set; }

        public virtual User Autor { get; set; }
        public virtual Beitrag Beitrag { get; set; }
        public virtual ICollection<KommentarLike> KommentarLikes { get; set; }
    }
}
