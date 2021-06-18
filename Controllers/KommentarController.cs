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
    public class KommentarController : ControllerBase
    {
        private readonly ILogger<BeitragController> _logger;

        private ApplicationDbContext _context;

        public KommentarController(ILogger<BeitragController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("Kommentar/{id}")]
        public Kommentar Get(int id)
        {
            var Kommentar = _context.Kommentars
                .Where(k => k.Id == id)
                .FirstOrDefault();
            return Kommentar;
        }

        [HttpPost]
        [Route("Kommentar/{id}")]
        public void DeleteComment(int id)
        {
            var toRemove = _context.Kommentars
                .Where(k => k.Id == id);
            _context.Remove(toRemove);
        }

        [HttpPost]
        [Route("Kommentar")]
        public Kommentar CreateOrUpdate(Kommentar kommentar)
        {
            var dbKommentar = _context.Kommentars
                .Where(k => k.Id == kommentar.Id)
                .FirstOrDefault();
            if (dbKommentar != null)
            {
                dbKommentar.Inhalt = kommentar.Inhalt;
                dbKommentar.Titel = kommentar.Titel;
            }
            else
            {
                dbKommentar = kommentar;
                _context.Kommentars
                    .Add(dbKommentar);
            }
            _context.SaveChanges();
            return dbKommentar;
        }
    }
}
