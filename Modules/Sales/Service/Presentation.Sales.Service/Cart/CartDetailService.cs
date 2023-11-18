using Blazored.LocalStorage;
using Presentation.Core.Service;
using Presentation.Sales.Domain.Cart;
using System.Text.Json;

namespace Presentation.Sales.Service.Cart
{
    public class CartDetailService : ApiClient, ICartDetailService
    {
        public CartDetailService(HttpClient httpClient, ILocalStorageService localStorageService) : base(httpClient,
            localStorageService)
        {
        }

        public async Task<HttpResponseMessage> CreateCartdetail(CartDetailModel cartDetails)
        {
            var result = await this.PostAsync(Endpoint.CREATE_CARTDETAIL, cartDetails);
            return result;
        }

        public async Task<HttpResponseMessage> DeleteCartdetail(CartDetailModel cartDetails)
        {
            var response = await this.PutAsync(Endpoint.DELETE_CARTDETAIL, cartDetails);
            return response;
        }

        public async Task<HttpResponseMessage> GetCartdetail()
        {
            var result = await this.GetAsync(Endpoint.GET_CARTDETAIL);
            return result;
        }

        public async Task<HttpResponseMessage> GetProductdetailId(int productid, int colorid, int sizeid)
        {
            var param = new Dictionary<string, object>
            {
                { "productid", productid }
            };
            var param1 = new Dictionary<string, object>
            {
                { "colorid", colorid }
            };
            var param2 = new Dictionary<string, object>
            {
                { "sizeid", sizeid }
            };
            var result = await this.GetAsync($"{Endpoint.GET_PRODUCTDETAILID}/{productid}/{colorid}/{sizeid}");
            return result;
        }

        public async Task<HttpResponseMessage> UpdateCartdetail(CartDetailModel cartDetails)
        {
            var result = await this.PutAsync(Endpoint.UPDATE_CARTDETAIL, cartDetails);
            return result;
        }
    }
}