using Microsoft.Extensions.DependencyInjection;
using Presentation.Sales.Domain.Cart;
using Presentation.Sales.Service.Cart;

namespace Presentation.Sales.Service;

public static class ServiceCollectionExtensions
{
    public static void AddSalesService(this IServiceCollection services)
    {
        services.AddScoped<ICartDetailService, CartDetailService>();
    }
}