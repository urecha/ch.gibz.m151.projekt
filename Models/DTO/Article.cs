using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ch.gibz.m151.projekt.Models.DTO
{
    public class Article
    {
        public int Id { get; set; }
        public UserSummary Autor { get; set; }
        public string Titel { get; set; }
        public DateTime Datum { get; set; }
        public string Inhalt { get; set; }
        public virtual ICollection<ArticleLike> BeitragLikes { get; set; }
        public virtual ICollection<Comment> Kommentare { get; set; }

        public Article()
        {

        }

        public Article(Beitrag beitrag)
        {
            this.Id = beitrag.Id;
            this.Autor = new UserSummary(beitrag.Autor);
            this.Titel = beitrag.Titel;
            this.Datum = beitrag.ErstelltAm;
            this.Inhalt = beitrag.Inhalt;
            this.BeitragLikes = new List<ArticleLike>();
            foreach (BeitragLike like in beitrag.BeitragLikes)
            {
                this.BeitragLikes.Add(new ArticleLike(like));
            }
            this.Kommentare = new List<Comment>(beitrag.Kommentars.Select(k => new Comment(k)));
        }
    }
}
