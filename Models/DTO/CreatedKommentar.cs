using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ch.gibz.m151.projekt.Models.DTO
{
    public class CreatedKommentar
    {
        public CreatedKommentar(Kommentar kommentar, int kommentarId)
        {
            KommentarId = kommentarId;
            Kommentar = kommentar;
        }

        public CreatedKommentar(Kommentar kommentar)
        {
            Kommentar = kommentar;
        }

        public int KommentarId { get; set; }
        public Kommentar Kommentar { get; set; }
    }
}
