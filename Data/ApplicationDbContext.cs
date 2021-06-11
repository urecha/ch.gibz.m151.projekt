using ch.gibz.m151.projekt.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ch.gibz.m151.projekt.Data
{
    public partial class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public virtual DbSet<Beitrag> Beitrags { get; set;  }
        public virtual DbSet<BeitragDatei> BeitragDateis { get; set; }
        public virtual DbSet<BeitragLike> BeitragLikes { get; set; }
        public virtual DbSet<Datei> Dateis { get; set; }
        public virtual DbSet<Kommentar> Kommentars { get; set; }
        public virtual DbSet<KommentarLike> KommentarLikes { get; set; }
    }
}
