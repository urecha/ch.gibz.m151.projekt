using ch.gibz.m151.projekt.Data;
using ch.gibz.m151.projekt.Models;
using ch.gibz.m151.projekt.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
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

        private readonly IHttpContextAccessor _httpContextAccessor;

        public BeitragController(ILogger<BeitragController> logger, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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
            var allArticles = _context.Beitrags
                .Include(b => b.BeitragLikes)
                .ThenInclude(bl => bl.User)
                .Include(b => b.Autor)
                .ToList();

            var hottestArticles = allArticles.OrderByDescending(b => b.GetTotalLikes());

            count = count >= hottestArticles.Count() ? hottestArticles.Count() - 1 : count;

            return Ok(hottestArticles.Take(count).Select(a => new ArticleSummary(a)));
        }

        [HttpGet]
        [Route("shittiest")]
        public IActionResult GetShittiest(int count = 10)
        {
            var allArticles = _context.Beitrags
                .Include(b => b.BeitragLikes)
                .ThenInclude(bl => bl.User)
                .Include(b => b.Autor)
                .ToList();

            var shittiestArticles = allArticles.OrderBy(b => b.GetTotalLikes());
            count = count >= shittiestArticles.Count() ? shittiestArticles.Count() - 1 : count;

            return Ok(shittiestArticles.Take(count).Select(a => new ArticleSummary(a)));
        }

        [HttpGet]
        public IActionResult GetSummaries(int count = 10)
        {
            var LatestArticles = _context.Beitrags
                .Include(b => b.Autor)
                .OrderByDescending(b => b.ErstelltAm)
                .ToList();

            count = count >= LatestArticles.Count() ? LatestArticles.Count() - 1 : count;

            return Ok(LatestArticles.Take(count).Select(a => new ArticleSummary(a)));
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateOrUpdate([FromBody] Article incomingArticle)
        {
            ApplicationUser currentUser = GetApplicationUser();
            Article article = incomingArticle;
            article.Autor = new UserSummary(currentUser);
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
                dbBeitrag = new Beitrag(article, currentUser);
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

        private ApplicationUser GetApplicationUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _context.Users.Where(u => u.Id == userId).FirstOrDefault();
        }
    }
}
