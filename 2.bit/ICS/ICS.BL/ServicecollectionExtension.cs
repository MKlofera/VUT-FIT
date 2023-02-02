using AutoMapper;
using AutoMapper.EquivalencyExpression;
using ICS.BL.Facades;
using ICS.DAL;
using ICS.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace ICS.BL;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBLServices(this IServiceCollection services)
    {
        services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();
        services.AddSingleton<CarpoolsFacade>();
        services.AddSingleton<CarsFacade>();
        services.AddSingleton<RidesFacade>();
        services.AddSingleton<UsersFacade>();

        services.AddAutoMapper((serviceProvider, cfg) =>
        {
            cfg.AddCollectionMappers();

            var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<ICSDbContext>>();
            using var dbContext = dbContextFactory.CreateDbContext();
            cfg.UseEntityFrameworkCoreModel<ICSDbContext>(dbContext.Model);
        }, typeof(BusinessLogic).Assembly);
        return services;
    }
}