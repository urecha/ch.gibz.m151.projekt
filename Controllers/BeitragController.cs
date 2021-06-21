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
using System.Text.Json;
using System.Threading.Tasks;

namespace ch.gibz.m151.projekt.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
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
        [Route("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var Beitrag = _context.Beitrags
                .Where(b => b.Id == id)
                .Include(b => b.Autor)
                .FirstOrDefault();

            return Ok(new Article(Beitrag));
        }

        [HttpGet]
        [Route("hottest")]
        public IActionResult GetHottest(int count = 10)
        {
            var HottestArticles = _context.Beitrags
                .OrderByDescending(b => b.BeitragLikes)
                .ToList();

            count = count >= HottestArticles.Count() ? HottestArticles.Count() - 1 : count;

            return Ok(HottestArticles.Take(count).Select(a => new ArticleSummary(a)));
        }

        [HttpGet]
        [Route("shittiest")]
        public IActionResult GetShittiest(int count = 10)
        {
            var ShittiestArticles = _context.Beitrags
                .OrderBy(b => b.BeitragLikes)
                .ToList();

            count = count >= ShittiestArticles.Count() ? ShittiestArticles.Count() - 1 : count;

            return Ok(ShittiestArticles.Take(count).Select(a => new ArticleSummary(a)));
        }

        [HttpGet]
        public IActionResult GetSummaries(int count = 10)
        {
            var LatestArticles = _context.Beitrags
                .Include(b => b.Autor)
                .OrderBy(b => b.ErstelltAm)
                .ToList();

            count = count >= LatestArticles.Count() ? LatestArticles.Count() - 1 : count;

            return Ok(LatestArticles.Take(count).Select(a => new ArticleSummary(a)));
        }

        [HttpPost]
        public IActionResult CreateOrUpdate([FromBody] string articleJson)
        {
            Article article = JsonSerializer.Deserialize<Article>(articleJson);
            var dbBeitrag = _context.Beitrags
                .Where(b => b.Id == article.Id)
                .FirstOrDefault();
            if (dbBeitrag != null)
            {
                dbBeitrag.Inhalt = article.Inhalt;
                dbBeitrag.Titel = article.Titel;
                _context.SaveChanges();
                return Ok(new Article(dbBeitrag));
            }
            else
            {
                dbBeitrag = new Beitrag(article);
                _context.Beitrags
                    .Add(dbBeitrag);
                _context.SaveChanges();
                return CreatedAtAction(nameof(CreateOrUpdate), new { id = dbBeitrag.Id }, new Article(dbBeitrag));
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteArticle([FromRoute] int id)
        {
            var toRemove = _context.Beitrags
                .Where(b => b.Id == id);
            _context.Remove(toRemove);

            _context.SaveChanges();
            return Ok();
        }
    }
}
