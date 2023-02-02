using ICS.DAL;
using Microsoft.EntityFrameworkCore;

namespace ICS.Common.Tests.Factories;

public class DbContextSQLTestingFactory : IDbContextFactory<ICSDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextSQLTestingFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }
    public ICSDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<ICSDbContext> builder = new();
        // TODO change connection string to variable
        builder.UseSqlServer($"Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog = {_databaseName};MultipleActiveResultSets = True;Integrated Security = True;");
        //builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");

        //contextOptionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        //builder.EnableSensitiveDataLogging();

        return new ICSTestingDbContext(builder.Options, _seedTestingData);
    }
}