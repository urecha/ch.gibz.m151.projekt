using System;
using System.Collections.Generic;

#nullable disable

namespace ch.gibz.m151.projekt.Models
{
    public partial class BeitragLike
    {
        public BeitragLike() { }
        public BeitragLike(Beitrag beitrag, ApplicationUser user, bool isDislike)
        {
            Beitrag = beitrag;
            User = user;
            IstDislike = isDislike;
        }

        public int Id { get; set; }
        public bool? IstDislike { get; set; }

        public Beitrag Beitrag { get; set; }
        public ApplicationUser User { get; set; }
    }
}
