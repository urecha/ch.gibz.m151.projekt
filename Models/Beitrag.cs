using ch.gibz.m151.projekt.Models.DTO;
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

        public Beitrag(Article article)
        {
            BeitragLikes = new HashSet<BeitragLike>();
            Kommentars = new HashSet<Kommentar>();

            Id = article.Id;
            Titel = article.Titel;
            ErstelltAm = article.Datum;
            Inhalt = article.Inhalt;
            Autor.Id = article.Autor.Id;
        }

        public int Id { get; set; }
        public string Titel { get; set; }
        public DateTime ErstelltAm { get; set; }
        public string Inhalt { get; set; }

        public ApplicationUser Autor { get; set; }
        public ICollection<BeitragLike> BeitragLikes { get; set; }
        public ICollection<Kommentar> Kommentars { get; set; }
    }
}
