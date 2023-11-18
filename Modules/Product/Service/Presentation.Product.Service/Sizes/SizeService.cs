using Blazored.LocalStorage;
using Presentation.Core.Service;
using Presentation.Product.Domain.Sizes;

namespace Presentation.Product.Service.Sizes
{
    public class SizeService : ApiClient, ISizeService
    {
        public SizeService(HttpClient httpClient, ILocalStorageService localStorageService) : base(httpClient,
            localStorageService)
        {
        }

        public async Task<HttpResponseMessage> DeleteSize(SizeModel param)
        {
            var response = await this.PutAsync(Endpoint.DELETE_SIZE, param);

            return response;
        }

        public async Task<HttpResponseMessage> GetSize()
        {
            var result = await this.GetAsync(Endpoint.GET_SIZE);

            return result;
        }

        public async Task<HttpResponseMessage> UpdateSize(IEnumerable<SizeModel> param)
        {
            var request = param.Where(x => x.IsDeleted != 1);

            var response =
                await this.PostAsync(Endpoint.UPDATE_SIZE, new { Sizes = request });

            return response;
        }

        public async Task<HttpResponseMessage> ReactiveSize(SizeModel param)
        {
            var response = await this.PutAsync(Endpoint.REACTIVE_SIZE, param);

            return response;
        }
    }
}