using System;
using System.Collections.Generic;

#nullable disable

namespace ch.gibz.m151.projekt.Models
{
    public partial class BeitragDatei
    {
        public int Id { get; set; }
        public int BeitragId { get; set; }
        public int DateiId { get; set; }
    }
}
