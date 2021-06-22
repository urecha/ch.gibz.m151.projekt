using ch.gibz.m151.projekt.Data;
using ch.gibz.m151.projekt.Models;
using ch.gibz.m151.projekt.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ch.gibz.m151.projekt.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class KommentarController : ControllerBase
    {
        private readonly ILogger<BeitragController> _logger;

        private ApplicationDbContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public KommentarController(ILogger<BeitragController> logger, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetForArticle([FromRoute] int id)
        {
            var comments = _context.Kommentars
                .Where(k => k.Beitrag.Id == id)
                .Select(k => new Comment(k))
                .ToList();

            return Ok(comments);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteComment([FromRoute] int id)
        {
            var toRemove = _context.Kommentars
                .Where(k => k.Id == id);
            _context.Remove(toRemove);

            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateOrUpdate([FromBody] Comment incomingComment)
        {
            ApplicationUser currentUser = GetApplicationUser();
            Comment comment = incomingComment;
            comment.Autor = new UserSummary(currentUser);

            var dbKommentar = _context.Kommentars
                .Where(k => k.Id == incomingComment.Id)
                .FirstOrDefault();
            if (dbKommentar != null)
            {
                dbKommentar.Inhalt = incomingComment.Inhalt;
                dbKommentar.Titel = incomingComment.Titel;
                _context.SaveChanges();
                return Ok(new Comment(dbKommentar));
            }
            else
            {
                dbKommentar = new Kommentar(incomingComment, currentUser);
                _context.Kommentars
                    .Add(dbKommentar);
                _context.SaveChanges();
                return CreatedAtAction(nameof(CreateOrUpdate), new { id = dbKommentar.Id } ,new Comment(dbKommentar));
            }
        }

        private ApplicationUser GetApplicationUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _context.Users.Where(u => u.Id == userId).FirstOrDefault();
        }
    }
}
