using AutoMapper;
using AutoMapper.EquivalencyExpression;
using ICS.Common.Tests;
using ICS.Common.Tests.Factories;
using ICS.DAL;
using ICS.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace ICS.BL.Tests;

public class CRUDFacadeTestsBase : IAsyncLifetime
{
    protected CRUDFacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        DbContextFactory = new DbContextSQLTestingFactory(GetType().FullName!, seedTestingData: true);

        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);

        var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(new[]
                {
                    typeof(BusinessLogic),
                });
                cfg.AddCollectionMappers();

                using var dbContext = DbContextFactory.CreateDbContext();
                cfg.UseEntityFrameworkCoreModel<ICSDbContext>(dbContext.Model);
            }
        );
        Mapper = new Mapper(configuration);
        //Mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    protected IDbContextFactory<ICSDbContext> DbContextFactory { get; }

    protected Mapper Mapper { get; }

    protected UnitOfWorkFactory UnitOfWorkFactory { get; }

    public async Task InitializeAsync()
    {
        await using var db = await DbContextFactory.CreateDbContextAsync();
        await db.Database.EnsureDeletedAsync();
        await db.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var db = await DbContextFactory.CreateDbContextAsync();
        await db.Database.EnsureDeletedAsync();
    }
}