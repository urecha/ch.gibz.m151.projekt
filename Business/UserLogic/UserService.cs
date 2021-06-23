using ch.gibz.m151.projekt.Data;
using ch.gibz.m151.projekt.Models;
using ch.gibz.m151.projekt.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ch.gibz.m151.projekt.Business.UserLogic
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _context;

        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public User Get(string id)
        {
            var dbUser = _context.Users
                .Where(u => u.Id == id)
                .FirstOrDefault();
            var user = new User(dbUser);
            // Add rankings to user
            return user;
        }

        public List<User> GetTopBuenzli()
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

        public List<User> GetTopBuenzliArticles()
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

        public List<User> GetTopBuenzliComments()
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

        public List<User> GetTopHalbschueh()
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

        public List<User> GetTopHalbschuehArticles()
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

        public List<User> GetTopHalbschuehComments()
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

        private List<ApplicationUser> GetApplicationUsers()
        {
            return _context.Users
                .Include(u => u.BeitragLikes)
                .Include(u => u.KommentarLikes)
                .ToList();
        }
    }
}
