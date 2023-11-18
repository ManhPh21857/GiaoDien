﻿using Presentation.Common.Service;
using Presentation.HumanResources.Service;
using Presentation.Product.Service;
using Presentation.Sales.Service;

namespace WebClient.Admin
{
    public static class ServiceCollectionExtensions
    {
        public static void AddWebClient(this IServiceCollection service)
        {
            service.AddCommonService();
            service.AddHumanResourcesService();
            service.AddProductService();
            service.AddSalesService();
        }
    }
}