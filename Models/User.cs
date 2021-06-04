using System;
using System.Collections.Generic;

#nullable disable

namespace ch.gibz.m151.projekt.Models
{
    public partial class User
    {
        public User()
        {
            BeitragLikes = new HashSet<BeitragLike>();
            Beitrags = new HashSet<Beitrag>();
            KommentarLikes = new HashSet<KommentarLike>();
            Kommentars = new HashSet<Kommentar>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public virtual ICollection<BeitragLike> BeitragLikes { get; set; }
        public virtual ICollection<Beitrag> Beitrags { get; set; }
        public virtual ICollection<KommentarLike> KommentarLikes { get; set; }
        public virtual ICollection<Kommentar> Kommentars { get; set; }
    }
}
