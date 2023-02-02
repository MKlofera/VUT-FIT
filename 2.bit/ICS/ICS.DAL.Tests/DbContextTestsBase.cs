using System;
using System.Threading.Tasks;
using ICS.Common.Tests;
using ICS.Common.Tests.Factories;
using ICS.DAL.Factories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace ICS.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
    protected IDbContextFactory<ICSDbContext> DbContextFactory { get; }
    protected ICSDbContext ICSDbContextSUT { get; }
    protected DbContextTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        DbContextFactory = new DbContextSQLTestingFactory(GetType().FullName!, seedTestingData: true);

        ICSDbContextSUT = DbContextFactory.CreateDbContext();
    }

    public async Task InitializeAsync()
    {
        await ICSDbContextSUT.Database.EnsureDeletedAsync();
        await ICSDbContextSUT.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await ICSDbContextSUT.Database.EnsureDeletedAsync();
        await ICSDbContextSUT.DisposeAsync();
    }
}