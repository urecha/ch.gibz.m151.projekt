using ch.gibz.m151.projekt.Models.DTO;
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

        public Kommentar(Comment comment, ApplicationUser user)
        {
            this.Titel = comment.Titel;
            this.Inhalt = comment.Inhalt;
            this.Datum = comment.Datum;
            this.Autor = user;
            this.Beitrag.Id = comment.ArticleId;

            this.KommentarLikes = new HashSet<KommentarLike>();
        }

        public int Id { get; set; }
        public string Titel { get; set; }
        public string Inhalt { get; set; }
        public DateTime Datum { get; set; }

        public ApplicationUser Autor { get; set; }
        public Beitrag Beitrag { get; set; }
        public ICollection<KommentarLike> KommentarLikes { get; set; }
    }
}
