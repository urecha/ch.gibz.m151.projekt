using ch.gibz.m151.projekt.Business.UserLogic;
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
using System.Threading.Tasks;

namespace ch.gibz.m151.projekt.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        private ApplicationDbContext _context;

        private readonly UserService userService;

        public UserController(ILogger<UserController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            userService = new UserService(context, new HttpContextAccessor());
        }

        [HttpGet]
        [Route("User")]
        public User Get(string id)
        {
            var user = userService.Get(id);
            // Add rankings to user
            return user;
        }

        [HttpGet]
        [Route("User/top-buenzlis")]
        public IEnumerable<User> GetTopBuenzli()
        {
            return userService.GetTopBuenzli();
        }

        [HttpGet]
        [Route("User/top-buenzlis/articles")]
        public IEnumerable<User> GetTopBuenzliArticles()
        {
            return userService.GetTopBuenzliArticles();
        }

        [HttpGet]
        [Route("User/top-buenzlis/comments")]
        public IEnumerable<User> GetTopBuenzliComments()
        {
            return userService.GetTopBuenzliComments();
        }

        [HttpGet]
        [Route("User/top-halbschueh")]
        public IEnumerable<User> GetTopHalbschueh()
        {
            return userService.GetTopHalbschueh();
        }

        [HttpGet]
        [Route("User/top-halbschueh/articles")]
        public IEnumerable<User> GetTopHalbschuehArticles()
        {
            return userService.GetTopHalbschuehArticles();
        }

        [HttpGet]
        [Route("User/top-halbschueh/comments")]
        public IEnumerable<User> GetTopHalbschuehComments()
        {
            return userService.GetTopHalbschuehComments();
        }

        private List<ApplicationUser> GetApplicationUsers()
        {
            return _context.Users
                .Include(u => u.BeitragLikes)
                .Include(u => u.KommentarLikes)
                .ToList();
        }

        private List<User> SanitizeUsers(List<User> users)
        {
            foreach (User user in users)
            {
                foreach (KommentarLike like in user.CommentLikes)
                {
                    like.User = null;
                }
                foreach (BeitragLike like in user.ArticleLikes)
                {
                    like.User = null;
                }
            }
            return users;
        }
        private List<User> TransformAppUserToBuenzli(List<ApplicationUser> users)
        {
            var count = users.Count();
            var buenzlis = new List<User>();
            for (int i = 0; i < count; i++)
            {
                var currentBuenzli = new User(users[i]);
                buenzlis.Add(currentBuenzli);
            }
            return buenzlis;
        }

        private void AddRankingHalbschueh(List<User> buenzlis)
        {
            int currentRank = 1;
            foreach (User buenzli in buenzlis)
            {
                buenzli.HalbschuehRanking = currentRank;
                currentRank++;
            }
        }

        private void AddRankingBuenzli(List<User> buenzlis)
        {
            int currentRank = 1;
            foreach (User buenzli in buenzlis)
            {
                buenzli.BuenzliRanking = currentRank;
                currentRank++;
            }
        }

        
    }
}
