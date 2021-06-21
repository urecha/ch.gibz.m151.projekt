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
        public bool? IstDislike { get; set; }

        public Kommentar Kommentar { get; set; }
        public ApplicationUser User { get; set; }
    }
}
