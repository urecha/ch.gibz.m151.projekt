using ch.gibz.m151.projekt.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable

namespace ch.gibz.m151.projekt.Models
{
    public partial class Kommentar
    {
        public int GetTotalLikes()
        {
            return KommentarLikes.Where(kl => kl.IstDislike == false).ToList().Count;
        }

        public int GetTotalDislikes()
        {
            return KommentarLikes.Where(kl => kl.IstDislike == true).ToList().Count;
        }

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
            this.Beitrag = comment.Beitrag;
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
