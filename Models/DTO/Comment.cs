using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ch.gibz.m151.projekt.Models.DTO
{
    public class Comment
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public UserSummary Autor { get; set; }
        public Beitrag Beitrag { get; set; }
        public string Titel { get; set; }
        public string Inhalt { get; set; }
        public DateTime Datum { get; set; }
        public virtual ICollection<CommentLike> Likes { get; set; }

        public Comment(Kommentar kommentar)
        {
            this.Id = kommentar.Id;
            this.Autor = new UserSummary(kommentar.Autor);
            this.Titel = kommentar.Titel;
            this.Inhalt = kommentar.Inhalt;
            this.Datum = kommentar.Datum;
            this.Likes = new List<CommentLike>();
            foreach(KommentarLike like in kommentar.KommentarLikes)
            {
                this.Likes.Add(new CommentLike(like));
            }
        }

        public Comment()
        {

        }
    }
}
