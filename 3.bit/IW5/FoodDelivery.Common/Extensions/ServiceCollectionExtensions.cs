using FoodDelivery.Common.Installers;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInstaller<TInstaller>(this IServiceCollection serviceCollection)
        where TInstaller : IInstaller, new()
    {
        var installer = new TInstaller();
        installer.Install(serviceCollection);
    }
}
