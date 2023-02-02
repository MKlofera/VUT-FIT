﻿using FoodDelivery.Api.DAL.Common.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.Api.DAL.EF.Installers;

public class ApiDALEFInstaller
{
    public void Install(IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<FoodDeliveryDbContext>(options => options.UseSqlServer(connectionString));

        serviceCollection.Scan(selector =>
            selector.FromAssemblyOf<ApiDALEFInstaller>()
                .AddClasses(classes => classes.AssignableTo(typeof(IApiRepository<>)))
                .AsMatchingInterface()
                .WithScopedLifetime());
    }
}
