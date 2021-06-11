using System;
using System.Collections.Generic;

#nullable disable

namespace ch.gibz.m151.projekt.Models
{
    public partial class BeitragDatei
    {
        public int Id { get; set; }
        public virtual Beitrag Beitrag{ get; set; }
        public virtual Datei Datei { get; set; }
    }
}
