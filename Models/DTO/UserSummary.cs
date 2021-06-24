using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ch.gibz.m151.projekt.Models.DTO
{
    public class UserSummary
    {
        public UserSummary(ApplicationUser user)
        {
            Id = user.Id;
            Name = user.UserName;
        }

        public UserSummary()
        {

        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
