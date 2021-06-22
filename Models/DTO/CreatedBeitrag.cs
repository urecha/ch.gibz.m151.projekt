using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ch.gibz.m151.projekt.Models.DTO
{
    public class CreatedBeitrag
    {
        public CreatedBeitrag(Beitrag beitrag, int beitragId)
        {
            BeitragId = beitragId;
            Beitrag = beitrag;
        }

        public CreatedBeitrag(Beitrag beitrag)
        {
            Beitrag = beitrag;
        }

        public int BeitragId { get; set; }
        public Beitrag Beitrag { get; set; }
    }
}
