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
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        private ApplicationDbContext _context;

        public UserController(ILogger<UserController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        [Route("User")]
        public ApplicationUser Get(string id)
        {
            var user = _context.Users
                .Where(u => u.Id == id)
                .FirstOrDefault();
            return user;
        }

    }
}
