using DragonFly.AspNetCore.Identity.EF.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DragonFly.AspNetCore.Identity.EF
{
    /// <summary>
    /// DragonFlyIdentityContext
    /// </summary>
    public class DragonFlyIdentityContext : IdentityDbContext<DbUser, DbRole, Guid>
    {
        public DragonFlyIdentityContext(DbContextOptions<DragonFlyIdentityContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


        }
    }
}