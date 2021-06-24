using ch.gibz.m151.projekt.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ch.gibz.m151.projekt.Business.UserLogic
{
    interface IUserService
    {
        public User Get(string id);

        public List<User> GetTopBuenzli();

        public List<User> GetTopBuenzliArticles();

        public List<User> GetTopBuenzliComments();

        public List<User> GetTopHalbschueh();

        public List<User> GetTopHalbschuehArticles();

        public List<User> GetTopHalbschuehComments();
    }
}
