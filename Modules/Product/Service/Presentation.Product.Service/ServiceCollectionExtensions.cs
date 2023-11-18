using Microsoft.Extensions.DependencyInjection;
using Presentation.Product.Domain.Classifications;
using Presentation.Product.Domain.Colors;
using Presentation.Product.Domain.Materials;
using Presentation.Product.Domain.Origins;
using Presentation.Product.Domain.Products;
using Presentation.Product.Domain.Sizes;
using Presentation.Product.Domain.Suppliers;
using Presentation.Product.Domain.Trademarks;
using Presentation.Product.Service.Classifications;
using Presentation.Product.Service.Colors;
using Presentation.Product.Service.Materials;
using Presentation.Product.Service.Origins;
using Presentation.Product.Service.Products;
using Presentation.Product.Service.Sizes;
using Presentation.Product.Service.Suppliers;
using Presentation.Product.Service.Trademarks;

namespace Presentation.Product.Service;

public static class ServiceCollectionExtensions
{
    public static void AddProductService(this IServiceCollection services)
    {
        services.AddScoped<IClassificationService, ClassificationService>();
        services.AddScoped<IColorService, ColorService>();
        services.AddScoped<IMaterialService, MaterialService>();
        services.AddScoped<IOriginService, OriginService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ISizeService, SizeService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<ITrademarkService, TrademarkService>();
    }
}