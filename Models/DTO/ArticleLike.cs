using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ch.gibz.m151.projekt.Models.DTO
{
    public class ArticleLike
    {
        public int Id { get; set; }
        public bool IstDislike { get; set; }
        public int ArticleId { get; set; }
        public UserSummary User { get; set; }

        public ArticleLike(BeitragLike like)
        {
            this.Id = like.Id;
            this.IstDislike = like.IstDislike ?? false;
            this.ArticleId = like.Beitrag.Id;
            this.User = new UserSummary(like.User);
        }
    }

    public class ArticleLikePreview
    {
        public int Id { get; set; }
        public bool IstDislike { get; set; }
        public int ArticleId { get; set; }
        public string ArticleTitel { get; set; }
        public UserSummary User { get; set; }

        public ArticleLikePreview(BeitragLike like)
        {
            this.Id = like.Id;
            this.IstDislike = like.IstDislike ?? false;
            this.ArticleId = like.Beitrag.Id;
            this.ArticleTitel = like.Beitrag.Titel;
            this.User = new UserSummary(like.User);
        }
    }
}
