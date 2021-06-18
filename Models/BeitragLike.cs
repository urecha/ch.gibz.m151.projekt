using System;
using System.Collections.Generic;

#nullable disable

namespace ch.gibz.m151.projekt.Models
{
    public partial class BeitragLike
    {
        public int Id { get; set; }
        public bool? IstDislike { get; set; }

        public virtual Beitrag Beitrag { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
