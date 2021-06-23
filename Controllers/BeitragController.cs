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
using ch.gibz.m151.projekt.Business;

namespace ch.gibz.m151.projekt.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BeitragController : ControllerBase
    {
        private readonly ILogger<BeitragController> _logger;

        private ApplicationDbContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly BeitragService beitragService;

        public BeitragController(ILogger<BeitragController> logger, ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            beitragService = new BeitragService(context, httpContextAccessor);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var Beitrag = beitragService.GetBeitrag(id);
            return Ok(Beitrag);
        }

        [HttpGet]
        [Route("hottest")]
        public IActionResult GetHottest(int count = 10)
        {
            var hottestArticles = beitragService.GetHottest(count);
            return Ok(hottestArticles);
        }

        [HttpGet]
        [Route("shittiest")]
        public IActionResult GetShittiest(int count = 10)
        {
            var shittiestArticles = beitragService.GetShittiest(count);
            return Ok(shittiestArticles);
        }

        [HttpGet]
        public IActionResult GetSummaries(int count = 10)
        {
            var summaries = beitragService.GetSummaries(count);
            return Ok(summaries);
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateOrUpdate([FromBody] Article incomingArticle)
        {
            CreatedBeitrag createdBeitrag = beitragService.CreateOrUpdate(incomingArticle);
            return CreatedAtAction(nameof(CreateOrUpdate), new { id = createdBeitrag.BeitragId }, new Article(createdBeitrag.Beitrag));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteArticle([FromRoute] int id)
        {
            beitragService.DeleteArticle(id);
            return Ok();
        }

        [HttpGet]
        [Route("{id}/like")]
        [Authorize]
        public IActionResult LikeArticle(int id)
        {
            beitragService.LikeArticle(id);
            return Ok();
        }

        [HttpGet]
        [Route("{id}/dislike")]
        [Authorize]
        public IActionResult DislikeArticle(int id)
        {
            beitragService.DislikeArticle(id);
            return Ok();
        }

        private ApplicationUser GetApplicationUser()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return _context.Users.Where(u => u.Id == userId).FirstOrDefault();
        }
    }
}
