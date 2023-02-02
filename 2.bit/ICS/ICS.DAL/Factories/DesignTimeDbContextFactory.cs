using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace ICS.DAL.Factories
{
    /// <summary>
    /// EF Core CLI migration generation uses this DbContext to create model and migration
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ICSDbContext>
    {
        public ICSDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ICSDbContext> builder = new ();
            builder.UseSqlServer(
                @"Data Source=(LocalDB)\MSSQLLocalDB;
                Initial Catalog = ICS;
                MultipleActiveResultSets = True;
                Integrated Security = True; ");

            return new ICSDbContext(builder.Options);
        }
    }
}
