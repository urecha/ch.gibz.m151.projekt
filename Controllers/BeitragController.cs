using ch.gibz.m151.projekt.Data;
using ch.gibz.m151.projekt.Models;
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
        [Route("Beitrag/id")]
        public Beitrag Get(int id)
        {
            var Beitrag = _context.Beitrags
                .Where(b => b.Id == id)
                .FirstOrDefault();
            return Beitrag;
        }

        [HttpGet]
        [Route("Beitrag/hottest")]
        public IEnumerable<ArticleSummary> GetHottest(int count = 0)
        {
            var HottestArticles = _context.Beitrags
                .OrderByDescending(b => b.BeitragLikes)
                .ToList();
            var HottestSummaries = new List<ArticleSummary>();
            for (int i = count; i < count + 10; i++)
            {
                HottestSummaries.Add(new ArticleSummary(HottestArticles[i]));
            }
            return HottestSummaries;
        }

        [HttpGet]
        [Route("Beitrag/shittiest")]
        public IEnumerable<ArticleSummary> GetShittiest(int count)
        {
            var ShittiestArticles = _context.Beitrags
                .OrderBy(b => b.BeitragLikes)
                .ToList();
            var ShittiestSummaries = new List<ArticleSummary>();
            for (int i = count; i < count + 10; i++)
            {
                ShittiestSummaries.Add(new ArticleSummary(ShittiestArticles[i]));
            }
            return ShittiestSummaries;
        }

        [HttpGet]
        [Route("Beitrag")]
        public IEnumerable<ArticleSummary> GetSummaries(int count)
        {
            var LatestArticles = _context.Beitrags
                .Include(b => b.Autor)
                .OrderBy(b => b.ErstelltAm)
                .ToList();
            var LatestSummaries = new List<ArticleSummary>();
            for (int i = count; i < count + 10; i++)
            {
                LatestSummaries.Add(new ArticleSummary(LatestArticles[i]));
            }
            return LatestSummaries;
        }

        [HttpPost]
        [Route("Beitrag")]
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
