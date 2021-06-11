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
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class KommentarController : ControllerBase
    {
        private readonly ILogger<BeitragController> _logger;

        private BuenzliTreffContext _context = new BuenzliTreffContext();

        public KommentarController(ILogger<BeitragController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Beitrag> Get()
        {
            BuenzliTreffContext buenzliTreffContext = new BuenzliTreffContext();
            var Beitrags = buenzliTreffContext.Beitrags
                .Include(b => b.Autor)
                .Include(b => b.Titel)
                .Include(b => b.Inhalt)
                .Include(b => b.BeitragLikes)
                .ToList();
            return Beitrags;
        }

        [HttpGet]
        public IEnumerable<Beitrag> GetTop24h()
        {
            var Beitrags = _context.Beitrags
                .Include(b => b.Autor)
                .Include(b => b.Titel)
                .Include(b => b.Inhalt)
                .Include(b => b.BeitragLikes)
                .Include(b => b.ErstelltAm)
                .OrderBy(b => b.ErstelltAm)
                .Where(b => b.ErstelltAm < (DateTime.Now).AddDays(-1))
                .ToList();
            return Beitrags;
        }
    }
}
