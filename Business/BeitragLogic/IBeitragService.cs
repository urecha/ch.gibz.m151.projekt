using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ch.gibz.m151.projekt.Models;
using ch.gibz.m151.projekt.Models.DTO;

namespace ch.gibz.m151.projekt.Business.BeitragLogic
{
    public interface IBeitragService
    {
        public Article GetBeitrag(int id);
        public IEnumerable<ArticleSummary> GetHottest(int count = 10);

        public IEnumerable<ArticleSummary> GetShittiest(int count = 10);

        public IEnumerable<ArticleSummary> GetSummaries(int count = 10);

        public CreatedBeitrag CreateOrUpdate(Article incomingArticle);

        public void DeleteArticle(int id);

        public void LikeArticle(int id);

        public void DislikeArticle(int id);
    }
}
