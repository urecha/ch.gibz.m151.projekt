using ch.gibz.m151.projekt.Business.BeitragLogic;
using ch.gibz.m151.projekt.Data;
using ch.gibz.m151.projekt.Models;
using ch.gibz.m151.projekt.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ch.gibz.m151.projekt.Business
{
    public class BeitragService : IBeitragService
    {
        private ApplicationDbContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public BeitragService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public Article GetBeitrag(int id)
        {
            var Beitrag = _context.Beitrags
                .Where(b => b.Id == id)
                .Include(b => b.Autor)
                .Include(b => b.Kommentars)
                .ThenInclude(k => k.Autor)
                .FirstOrDefault();

            return new Article(Beitrag);
        }

        public IEnumerable<ArticleSummary> GetHottest(int count = 10)
        {
            var allArticles = _context.Beitrags
                .Include(b => b.BeitragLikes)
                .ThenInclude(bl => bl.User)
                .Include(b => b.Autor)
                .ToList();

            var hottestArticles = allArticles.OrderByDescending(b => b.GetTotalLikes());

            count = count > hottestArticles.Count() ? hottestArticles.Count() : count;

            return hottestArticles.Take(count).Select(a => new ArticleSummary(a));
        }

        public IEnumerable<ArticleSummary> GetShittiest(int count = 10)
        {
            var allArticles = _context.Beitrags
                .Include(b => b.BeitragLikes)
                .ThenInclude(bl => bl.User)
                .Include(b => b.Autor)
                .ToList();

            var shittiestArticles = allArticles.OrderBy(b => b.GetTotalLikes());
            count = count > shittiestArticles.Count() ? shittiestArticles.Count() : count;

            return shittiestArticles.Take(count).Select(a => new ArticleSummary(a));
        }

        public IEnumerable<ArticleSummary> GetSummaries(int count = 10)
        {
            var LatestArticles = _context.Beitrags
                .Include(b => b.Autor)
                .OrderByDescending(b => b.ErstelltAm)
                .ToList();

            count = count > LatestArticles.Count() ? LatestArticles.Count(): count;

            return LatestArticles.Take(count).Select(a => new ArticleSummary(a));
        }

        public CreatedBeitrag CreateOrUpdate(Article incomingArticle)
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
                return new CreatedBeitrag(dbBeitrag);
            }
            else
            {
                dbBeitrag = new Beitrag(article, currentUser);
                _context.Beitrags
                    .Add(dbBeitrag);
                _context.SaveChanges();
                return new CreatedBeitrag(dbBeitrag, dbBeitrag.Id);
            }
        }

        public void DeleteArticle(int id)
        {
            var toRemove = _context.Beitrags
                .Where(b => b.Id == id);
            _context.Remove(toRemove);

            _context.SaveChanges();
        }

        private ApplicationUser GetApplicationUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _context.Users.Where(u => u.Id == userId).FirstOrDefault();
        }

        public void LikeArticle(int id)
        {
            var toLike = _context.Beitrags.Where(b => b.Id == id).FirstOrDefault();
            BeitragLike newLike = new BeitragLike(toLike, GetApplicationUser(), false);
            toLike.BeitragLikes.Add(newLike);
            _context.SaveChanges();
        }

        public void DislikeArticle(int id)
        {
            var toLike = _context.Beitrags.Where(b => b.Id == id).FirstOrDefault();
            BeitragLike newLike = new BeitragLike(toLike, GetApplicationUser(), true);
            toLike.BeitragLikes.Add(newLike);
            _context.SaveChanges();
        }

    }
}
