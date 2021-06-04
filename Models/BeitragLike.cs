using System;
using System.Collections.Generic;

#nullable disable

namespace ch.gibz.m151.projekt.Models
{
    public partial class BeitragLike
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BeitragId { get; set; }
        public int? IstDislike { get; set; }

        public virtual Beitrag Beitrag { get; set; }
        public virtual User User { get; set; }
    }
}
