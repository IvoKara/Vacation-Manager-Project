using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Data.Entitiy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;

namespace Data.Context
{
    public class VacantionContext : IdentityDbContext<User, Role, int>
    {
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        //public virtual DbSet<Role> Roles { get; set; }

        public VacantionContext(DbContextOptions<VacantionContext> options) : base(options)
        { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         /*   modelBuilder.Entity<User>()
             .HasOne(b => b.Role)
             .WithMany(a => a.UsersInRole)
             .OnDelete(DeleteBehavior.SetNull);*/

            modelBuilder.Entity<User>()
             .HasOne(b => b.Team)
             .WithMany(a => a.Developers)
             .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Team>()
            .HasOne(b => b.WorkingOnProject)
            .WithMany(a => a.WorkingTeams)
            .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }        
    }
}
