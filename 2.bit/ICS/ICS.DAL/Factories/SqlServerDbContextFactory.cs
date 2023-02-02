using Microsoft.EntityFrameworkCore;

namespace ICS.DAL.Factories
{
    public class SqlServerDbContextFactory : IDbContextFactory<ICSDbContext>
    {
        private readonly string _connectionString;
        private readonly bool _seedDemoData;

        public SqlServerDbContextFactory(string connectionString, bool seedDemoData = false)
        {
            _connectionString = connectionString;
            _seedDemoData = seedDemoData;
        }

        public ICSDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ICSDbContext>();
            optionsBuilder.UseSqlServer(_connectionString);

            //optionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
            //optionsBuilder.EnableSensitiveDataLogging();

            return new ICSDbContext(optionsBuilder.Options, _seedDemoData);
        }
    }
}