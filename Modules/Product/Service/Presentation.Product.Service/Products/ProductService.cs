using Blazored.LocalStorage;
using Presentation.Core.Service;
using Presentation.Product.Domain.Products;

namespace Presentation.Product.Service.Products
{
    public class ProductService : ApiClient, IProductService
    {
        public ProductService(HttpClient httpClient, ILocalStorageService localStorageService) : base(httpClient, localStorageService)
        {
        }
        public async Task<HttpResponseMessage> GetProductView(int pageNo, ProductFilter filter)
        {
            var param = new Dictionary<string, object>
            {
                { "pageNo", pageNo }
            };

            filter = new ProductFilter
            {
                Name = "name"
            };

            var response = await this.PostAsync(GenerateApiUrl(Endpoint.GET_PRODUCT_VIEW, param), filter);

            return response;
        }

        public async Task<HttpResponseMessage> GetProductItem(int id)
        {
            var param = new Dictionary<string, object>
            {
                { "id", id }
            };

            var response = await this.GetAsync(GenerateApiUrl(Endpoint.GET_PRODUCT_ITEM, param));

            return response;
        }
        public async Task<HttpResponseMessage> GetProducts(int pageNo)
        {
            //var response = await HttpClient.GetAsync($"{Endpoint.GET_PRODUCT}/{pageNo}");
            var response = await this.GetAsync($"{Endpoint.GET_PRODUCT}/{pageNo}");

            return response;
        }

        public async Task<HttpResponseMessage> GetProduct(int id)
        {
            var param = new Dictionary<string, object>
            {
                { "id", id }
            };

            var response = await this.GetAsync(GenerateApiUrl(Endpoint.GET_PRODUCT_DETAIL, param));

            return response;
        }

        public async Task<HttpResponseMessage> CreateProduct(ProductModel request)
        {
            request.ProductColors = request.ProductColors.Where(x => x.Adoption).ToList();
            request.ProductSizes = request.ProductSizes.Where(x => x.Adoption).ToList();

            var response = await this.PostAsync(Endpoint.CREATE_PRODUCT, request);

            return response;
        }

        public async Task<HttpResponseMessage> DeleteProduct(int id)
        {
            var param = new Dictionary<string, object>
            {
                { "id", id }
            };

            var response = await this.DeleteAsync(GenerateApiUrl(Endpoint.DELETE_PRODUCT, param));

            return response;
        }
    }
}