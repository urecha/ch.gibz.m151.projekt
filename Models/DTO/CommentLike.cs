using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ch.gibz.m151.projekt.Models.DTO
{
    public class CommentLike
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int ArticleId { get; set; }
        public bool IstDislike { get; set; }
        public UserSummary User { get; set; }

        public CommentLike(KommentarLike like)
        {
            this.Id = like.Id;
            this.CommentId = like.Kommentar.Id;
            this.ArticleId = like.Kommentar.Beitrag.Id;
            this.IstDislike = like.IstDislike ?? false;
            this.User = new UserSummary(like.User);
        }
    }

    public class CommentLikePreview
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int ArticleId { get; set; }
        public string ArticleTitel { get; set; }
        public bool IstDislike { get; set; }
        public UserSummary User { get; set; }

        public CommentLikePreview(KommentarLike like)
        {
            this.Id = like.Id;
            this.CommentId = like.Kommentar.Id;
            this.ArticleId = like.Kommentar.Beitrag.Id;
            this.ArticleTitel = like.Kommentar.Beitrag.Titel;
            this.IstDislike = like.IstDislike ?? false;
            this.User = new UserSummary(like.User);
        }
    }
}
