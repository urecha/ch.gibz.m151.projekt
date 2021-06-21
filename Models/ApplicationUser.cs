using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ch.gibz.m151.projekt.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            BeitragLikes = new HashSet<BeitragLike>();
            Beitrags = new HashSet<Beitrag>();
            KommentarLikes = new HashSet<KommentarLike>();
            Kommentars = new HashSet<Kommentar>();
        }

        public string Name { get; set; }
        public string Password { get; set; }

        public ICollection<BeitragLike> BeitragLikes { get; set; }
        public ICollection<Beitrag> Beitrags { get; set; }
        public ICollection<KommentarLike> KommentarLikes { get; set; }
        public ICollection<Kommentar> Kommentars { get; set; }

        public int getArticleLikes()
        {
            return KommentarLikes
                .Where(al => al.IstDislike == false)
                .Count();
        }

        public int getCommentLikes()
        {
            return BeitragLikes
                .Where(cl => cl.IstDislike == false)
                .Count();
        }
    }

}
