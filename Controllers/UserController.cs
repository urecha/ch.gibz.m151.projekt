using ch.gibz.m151.projekt.Data;
using ch.gibz.m151.projekt.Models;
using ch.gibz.m151.projekt.Models.DTO;
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
        public User Get(string id)
        {
            var dbUser = _context.Users
                .Where(u => u.Id == id)
                .FirstOrDefault();
            var user = new User(dbUser);
            // Add rankings to user
            return user;
        }

        [HttpGet]
        [Route("User/top-buenzlis")]
        public IEnumerable<User> GetTopBuenzli()
        {
            var allBuenzlis = GetApplicationUsers();

            var topDbBuenzlis = allBuenzlis
                .OrderByDescending(u => (u.getArticleLikes() + u.getCommentLikes()))
                .ToList()
                .Take(3);

            var topBuenzlis = TransformAppUserToBuenzli(topDbBuenzlis.ToList());
            AddRankingBuenzli(topBuenzlis);
            return SanitizeUsers(topBuenzlis);
        }

        [HttpGet]
        [Route("User/top-buenzlis/articles")]
        public IEnumerable<User> GetTopBuenzliArticles()
        {
            var allBuenzlis = GetApplicationUsers();

            var topDbBuenzlis = allBuenzlis
                .OrderByDescending(u => u.getArticleLikes())
                .ToList()
                .Take(3);

            var topBuenzlis = TransformAppUserToBuenzli(topDbBuenzlis.ToList());
            AddRankingBuenzli(topBuenzlis);
            return SanitizeUsers(topBuenzlis);
        }

        [HttpGet]
        [Route("User/top-buenzlis/comments")]
        public IEnumerable<User> GetTopBuenzliComments()
        {
            var allBuenzlis = GetApplicationUsers();

            var topDbBuenzlis = allBuenzlis
                .OrderByDescending(u => u.getCommentLikes())
                .ToList()
                .Take(3);

            var topBuenzlis = TransformAppUserToBuenzli(topDbBuenzlis.ToList());
            AddRankingBuenzli(topBuenzlis);
            return SanitizeUsers(topBuenzlis);
        }

        [HttpGet]
        [Route("User/top-halbschueh")]
        public IEnumerable<User> GetTopHalbschueh()
        {
            var allBuenzlis = GetApplicationUsers();

            var topDbBuenzlis = allBuenzlis
                .OrderBy(u => (u.getArticleLikes() + u.getCommentLikes()))
                .ToList()
                .Take(3);

            var topBuenzlis = TransformAppUserToBuenzli(topDbBuenzlis.ToList());
            AddRankingHalbschueh(topBuenzlis);
            return SanitizeUsers(topBuenzlis);
        }

        [HttpGet]
        [Route("User/top-halbschueh/articles")]
        public IEnumerable<User> GetTopHalbschuehArticles()
        {
            var allBuenzlis = GetApplicationUsers();

            var topDbBuenzlis = allBuenzlis
                .OrderBy(u => u.getArticleLikes())
                .ToList()
                .Take(3);

            var topBuenzlis = TransformAppUserToBuenzli(topDbBuenzlis.ToList());
            AddRankingHalbschueh(topBuenzlis);
            return SanitizeUsers(topBuenzlis);
        }

        [HttpGet]
        [Route("User/top-halbschueh/comments")]
        public IEnumerable<User> GetTopHalbschuehComments()
        {
            var allBuenzlis = GetApplicationUsers();

            var topDbBuenzlis = allBuenzlis
                .OrderBy(u => u.getCommentLikes())
                .ToList()
                .Take(3);

            var topBuenzlis = TransformAppUserToBuenzli(topDbBuenzlis.ToList());
            AddRankingHalbschueh(topBuenzlis);
            return SanitizeUsers(topBuenzlis);
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
