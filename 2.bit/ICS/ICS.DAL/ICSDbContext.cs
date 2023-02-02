using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using ICS.DAL.Entity;
using ICS.DAL.Seeds;
using Microsoft.EntityFrameworkCore;

namespace ICS.DAL
{
    public class ICSDbContext : DbContext
    {
        private readonly bool _seedDemoData;

        public ICSDbContext(DbContextOptions contextOptions, bool seedDemoData = true)
            : base(contextOptions)
        {
            _seedDemoData = seedDemoData;
        }

        public DbSet<UsersEntity> Users => Set<UsersEntity>();
        public DbSet<CarsEntity> Cars => Set<CarsEntity>();
        public DbSet<RidesEntity> Rides => Set<RidesEntity>();
        public DbSet<CarpoolsEntity> Carpools => Set<CarpoolsEntity>();



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarsEntity>()
                .HasMany(i => i.Rides)
                .WithOne(i => i.Car)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RidesEntity>()
                .HasMany(i => i.Carpoolers)
                .WithOne(i => i.Ride)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UsersEntity>()
                .HasMany(i => i.Rides)
                .WithOne(i => i.Driver)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UsersEntity>()
                .HasMany(i => i.Carpools)
                .WithOne(i => i.Codriver)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UsersEntity>()
                .HasMany(i => i.Cars)
                .WithOne(i => i.Owner)
                .OnDelete(DeleteBehavior.Cascade);

            if (_seedDemoData)
            {
                UsersSeeds.Seed(modelBuilder);
                CarsSeeds.Seed(modelBuilder);
                RidesSeeds.Seed(modelBuilder);
                CarpoolsSeeds.Seed(modelBuilder);
            }
        }

    }
}