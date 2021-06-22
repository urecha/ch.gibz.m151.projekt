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


namespace ch.gibz.m151.projekt.Business.KommentarLogic
{
    public class KommentarService : IKommentarService
    {
        private ApplicationDbContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public KommentarService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public CreatedKommentar CreateOrUpdate(Comment incomingComment)
        {
            ApplicationUser currentUser = GetApplicationUser();
            Comment comment = incomingComment;
            comment.Autor = new UserSummary(currentUser);
            comment.Beitrag = GetBeitrag(comment.ArticleId);

            var dbKommentar = _context.Kommentars
                .Where(k => k.Id == incomingComment.Id)
                .FirstOrDefault();
            if (dbKommentar != null)
            {
                dbKommentar.Inhalt = incomingComment.Inhalt;
                dbKommentar.Titel = incomingComment.Titel;
                _context.SaveChanges();
                return new CreatedKommentar(dbKommentar);
            }
            else
            {
                dbKommentar = new Kommentar(comment, currentUser);
                _context.Kommentars
                    .Add(dbKommentar);
                _context.SaveChanges();
                return new CreatedKommentar(dbKommentar, dbKommentar.Id);
            }
        }

        public void DeleteComment(int id)
        {
            var toRemove = _context.Kommentars
                .Where(k => k.Id == id);
            _context.Remove(toRemove);

            _context.SaveChanges();
        }

        public List<Comment> GetForArticle(string id)
        {
            var comments = _context.Kommentars
                .Where(k => k.Beitrag.Id == int.Parse(id))
                .Select(k => new Comment(k))
                .ToList();

            return comments;
        }

        private ApplicationUser GetApplicationUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _context.Users.Where(u => u.Id == userId).FirstOrDefault();
        }

        public Beitrag GetBeitrag(int id)
        {
            return _context.Beitrags.Where(b => b.Id == id).FirstOrDefault();
        }
    }
}
