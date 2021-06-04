using System;
using System.Collections.Generic;

#nullable disable

namespace ch.gibz.m151.projekt.Models
{
    public partial class Datei
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] File { get; set; }
    }
}
