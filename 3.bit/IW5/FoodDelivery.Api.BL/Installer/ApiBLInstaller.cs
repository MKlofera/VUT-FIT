using FoodDelivery.Common.BL;
using FoodDelivery.Common.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.Api.BL.Installer;

public class ApiBLInstaller : IInstaller
{
    public void Install(IServiceCollection serviceCollection)
    {
        serviceCollection.Scan(selector =>
            selector.FromAssemblyOf<ApiBLInstaller>()
                .AddClasses(classes => classes.AssignableTo(typeof(IAppFacade)))
                .AsMatchingInterface()
                .WithScopedLifetime());
    }
}