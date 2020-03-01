using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Data.Entitiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Data.Context
{
    public class VacantionContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        //public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        //public DbSet<IdentityUserClaim<int>> IdentityUserClaims { get; set; }

        public VacantionContext(DbContextOptions<VacantionContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
            /*modelBuilder.Entity<Team>()
                .HasMany(u => u.Developers)
                .WithOne(t => t.Team)
                .IsRequired();

            modelBuilder.Entity<Project>()
                .HasMany(t => t.WorkingTeams)
                .WithOne(p => p.WorkingOnProject)
                .IsRequired();*/
        }        
    }
}
