using ch.gibz.m151.projekt.Business.KommentarLogic;
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

        private readonly KommentarService kommentarService;

        public KommentarController(ILogger<BeitragController> logger, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            kommentarService = new KommentarService(context, httpContextAccessor);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetForArticle([FromRoute] string id)
        {
            var comments = kommentarService.GetForArticle(id);
            return Ok(comments);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize]
        public IActionResult DeleteComment([FromRoute] int id)
        {
            kommentarService.DeleteComment(id);
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateOrUpdate([FromBody] Comment incomingComment)
        {
            CreatedKommentar createdComment = kommentarService.CreateOrUpdate(incomingComment);
            return CreatedAtAction(nameof(CreateOrUpdate), new { id = createdComment.KommentarId }, new Comment(createdComment.Kommentar));
        }

        [HttpGet]
        [Route("{id}/like")]
        [Authorize]
        public IActionResult LikeComment(int id)
        {
            kommentarService.LikeComment(id);
            return Ok();
        }

        [HttpGet]
        [Route("{id}/dislike")]
        [Authorize]
        public IActionResult DislikeComment(int id)
        {
            kommentarService.DislikeComment(id);
            return Ok();
        }

        private ApplicationUser GetApplicationUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _context.Users.Where(u => u.Id == userId).FirstOrDefault();
        }

        private Beitrag GetBeitrag(int id)
        {
            return kommentarService.GetBeitrag(id);
        }
    }
}
