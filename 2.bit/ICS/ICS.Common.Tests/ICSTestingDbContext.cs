using ICS.Common.Tests.Seeds;
using ICS.DAL;
using Microsoft.EntityFrameworkCore;

namespace ICS.Common.Tests
{
    public class ICSTestingDbContext : ICSDbContext
    {
        private readonly bool _seedTestingData;

        public ICSTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false)
            : base(contextOptions, seedDemoData: false)
        {
            _seedTestingData = seedTestingData;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (_seedTestingData)
            {
                CarsSeeds.Seed(modelBuilder);
                RidesSeeds.Seed(modelBuilder);
                CarpoolsSeeds.Seed(modelBuilder);
                UsersSeeds.Seed(modelBuilder);
            }
        }
    }
}
