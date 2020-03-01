using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Data.Entitiy;

namespace Data.Context
{
    public class VacantionContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<Project> Projects { get; set; }

        public VacantionContext(DbContextOptions<VacantionContext> options) : base(options)
        { }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>()
                .HasMany(u => u.Developers)
                .WithOne(t => t.Team)
                .IsRequired();

            modelBuilder.Entity<Project>()
                .HasMany(t => t.WorkingTeams)
                .WithOne(p => p.WorkingOnProject)
                .IsRequired();
        }*/
    }
}
