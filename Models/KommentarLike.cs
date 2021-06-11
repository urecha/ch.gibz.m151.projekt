using System;
using System.Collections.Generic;

#nullable disable

namespace ch.gibz.m151.projekt.Models
{
    public partial class KommentarLike
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int KommentarId { get; set; }
        public int? IstDislike { get; set; }

        public virtual Kommentar Kommentar { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
