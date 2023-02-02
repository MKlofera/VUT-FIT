using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FoodDelivery.Api.DAL.EF.Factories;

public class FoodDeliveryDbContextFactory : IDesignTimeDbContextFactory<FoodDeliveryDbContext>
{
    public FoodDeliveryDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddUserSecrets<FoodDeliveryDbContextFactory>(optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<FoodDeliveryDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        return new FoodDeliveryDbContext(optionsBuilder.Options);
    }
}
