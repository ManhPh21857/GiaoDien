using Microsoft.Extensions.DependencyInjection;
using Presentation.Core.Domain;

namespace Presentation.Core.Service;

public static class ServiceCollectionExtensions {
    public static void AddCoreService(this IServiceCollection service) {
        service.AddCoreDomain();

        service.AddScoped<ISessionInfo, SessionInfo>();
    }
}