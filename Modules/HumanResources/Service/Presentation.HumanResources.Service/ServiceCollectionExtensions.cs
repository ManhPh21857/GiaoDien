using Microsoft.Extensions.DependencyInjection;
using Presentation.HumanResources.Domain.Authentication;
using Presentation.HumanResources.Service.Authentication;

namespace Presentation.HumanResources.Service;

public static class ServiceCollectionExtensions {
    public static void AddHumanResourcesService(this IServiceCollection services) {
        services.AddScoped<IAuthenticationService,AuthenticationService>();

    }
}