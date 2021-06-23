using ch.gibz.m151.projekt.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace ch.gibz.m151.projekt.Models
{
    public partial class Beitrag
    {
        public int GetTotalLikes()
        {
            return BeitragLikes.Where(bl => bl.IstDislike == false).ToList().Count;
        }

        public int GetTotalDislikes()
        {
            return BeitragLikes.Where(bl => bl.IstDislike == true).ToList().Count;
        }

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

        public Beitrag(Article article, ApplicationUser user)
        {
            BeitragLikes = new HashSet<BeitragLike>();
            Kommentars = new HashSet<Kommentar>();

            Titel = article.Titel;
            ErstelltAm = article.Datum;
            Inhalt = article.Inhalt;
            Autor = user;
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
