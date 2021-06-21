using ch.gibz.m151.projekt.Data;
using ch.gibz.m151.projekt.Models;
using ch.gibz.m151.projekt.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ch.gibz.m151.projekt.Controllers
{
    [ApiController]
    public class BeitragController : ControllerBase
    {
        private readonly ILogger<BeitragController> _logger;

        private ApplicationDbContext _context;

        public BeitragController(ILogger<BeitragController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("Beitrag/{id}")]
        public Article Get(int id)
        {
            var Beitrag = _context.Beitrags
                .Where(b => b.Id == id)
                .Include(b => b.Autor)
                .FirstOrDefault();

            return new Article(Beitrag);
        }

        [HttpGet]
        [Route("Beitrag/hottest")]
        public IEnumerable<ArticleSummary> GetHottest(int count = 10)
        {
            var HottestArticles = _context.Beitrags
                .OrderByDescending(b => b.BeitragLikes)
                .ToList();

            count = count >= HottestArticles.Count() ? HottestArticles.Count() - 1 : count;

            return HottestArticles.Take(count).Select(a => new ArticleSummary(a));
        }

        [HttpGet]
        [Route("Beitrag/shittiest")]
        public IEnumerable<ArticleSummary> GetShittiest(int count = 10)
        {
            var ShittiestArticles = _context.Beitrags
                .OrderBy(b => b.BeitragLikes)
                .ToList();

            count = count >= ShittiestArticles.Count() ? ShittiestArticles.Count() - 1 : count;

            return ShittiestArticles.Take(count).Select(a => new ArticleSummary(a));
        }

        [HttpGet]
        [Route("Beitrag")]
        public IEnumerable<ArticleSummary> GetSummaries(int count = 10)
        {
            var LatestArticles = _context.Beitrags
                .Include(b => b.Autor)
                .OrderBy(b => b.ErstelltAm)
                .ToList();

            count = count >= LatestArticles.Count() ? LatestArticles.Count() - 1 : count;

            return LatestArticles.Take(count).Select(a => new ArticleSummary(a));
        }

        [HttpPost]
        [Route("Beitrag")]
        //TODO create a "toModel" or "toEntity" method in class "Article". Because here, you're gonna get an article, not a "Beitrag" and you'll have to convert it.
        public Beitrag CreateOrUpdate(Beitrag beitrag)
        {
            var dbBeitrag = _context.Beitrags
                .Where(b => b.Id == beitrag.Id)
                .FirstOrDefault();
            if (dbBeitrag != null)
            {
                dbBeitrag.Inhalt = beitrag.Inhalt;
                dbBeitrag.Titel = beitrag.Titel;
            }
            else
            {
                dbBeitrag = beitrag;
                _context.Beitrags
                    .Add(dbBeitrag);
            }
            _context.SaveChanges();
            return dbBeitrag;
        }

        [HttpPost]
        [Route("Beitrag/{id}")]
        public void DeleteArticle(int id)
        {
            var toRemove = _context.Beitrags
                .Where(b => b.Id == id);
            _context.Remove(toRemove);
        }
    }
}
