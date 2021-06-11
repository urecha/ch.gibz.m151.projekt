//using ch.gibz.m151.projekt.Models;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ch.gibz.m151.projekt.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class BeitragController : ControllerBase
//    {
//        private readonly ILogger<BeitragController> _logger;

//        private BuenzliTreffContext _context = new BuenzliTreffContext();

//        public BeitragController(ILogger<BeitragController> logger)
//        {
//            _logger = logger;
//        }

//        [HttpGet]
//        public IEnumerable<Beitrag> Get()
//        {
//            BuenzliTreffContext buenzliTreffContext = new BuenzliTreffContext();
//            var Beitrags = buenzliTreffContext.Beitrags
//                .ToList();
//            return Beitrags;
//        }
//    }
//}
