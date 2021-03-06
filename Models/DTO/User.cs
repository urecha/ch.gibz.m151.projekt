using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ch.gibz.m151.projekt.Models.DTO
{
    public class User
    {
        public User(ApplicationUser user)
        {
            Id = user.Id;
            Name = user.UserName;
            ArticleLikes = user.BeitragLikes;
            CommentLikes = user.KommentarLikes;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<BeitragLike> ArticleLikes { get; set; }
        public ICollection<KommentarLike> CommentLikes { get; set; }

        public int BuenzliRanking { get; set; }
        public int HalbschuehRanking { get; set; }

        public int TotalLikes
        {
            get
            {
                var totalLikes = getArticleLikes() + getCommentLikes();
                return totalLikes;
            }
        }

        public int getArticleLikes()
        {
            return ArticleLikes
                .Where(al => al.IstDislike == false)
                .Count();
        }

        public int getCommentLikes()
        {
            return CommentLikes
                .Where(cl => cl.IstDislike == false)
                .Count();
        }

    }
}
