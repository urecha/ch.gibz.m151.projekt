using ch.gibz.m151.projekt.Models.DTO;
using System;
using System.Collections.Generic;

#nullable disable

namespace ch.gibz.m151.projekt.Models
{
    public partial class ArticleSummary
    {
        public ArticleSummary(Beitrag beitrag)
        {
            BeitragLikes = new HashSet<BeitragLike>();
            Id = beitrag.Id;
            Autor = new UserSummary(beitrag.Autor);
            Titel = beitrag.Titel;
            InhaltPreview = CreatePreview(beitrag.Inhalt);
            Datum = beitrag.ErstelltAm;
        }

        public int Id { get; set; }
        public UserSummary Autor { get; set; }
        public string Titel { get; set; }
        public string InhaltPreview { get; set; }
        public DateTime Datum { get; set; }
        public virtual ICollection<BeitragLike> BeitragLikes { get; set; }

        private string CreatePreview(string content)
        {
            int length = content.Length;
            length = length > 200 ? 200 : length;
            return content.Substring(0, length);
        }
    }
}
