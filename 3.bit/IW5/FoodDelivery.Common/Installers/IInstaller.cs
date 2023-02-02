using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.Common.Installers;

public interface IInstaller
{
    void Install(IServiceCollection serviceCollection);
}
