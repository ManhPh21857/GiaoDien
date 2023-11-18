using Blazored.LocalStorage;
using Presentation.Core.Service;
using Presentation.Product.Domain.Origins;

namespace Presentation.Product.Service.Origins
{
    public class OriginService : ApiClient, IOriginService
    {
        public OriginService(HttpClient httpClient, ILocalStorageService localStorageService) : base(httpClient, localStorageService)
        {
        }

        public async Task<HttpResponseMessage> DeleteOrigin(OriginModel origin)
        {
            var result = await this.PutAsync(Endpoint.DELETE_ORIGIN, origin);
            return result;

        }

        public async Task<HttpResponseMessage> GetOrigin()
        {
            var result = await this.GetAsync(Endpoint.GET_ORIGIN);

            return result;
        }

        public async Task<HttpResponseMessage> ReactiveOrigin(OriginModel origin)
        {
            var response = await this.PutAsync(Endpoint.REACTIVE_ORIGIN, origin);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateOrigin(IEnumerable<OriginModel> origin)
        {
            var request = origin.Where(x => x.IsDeleted != 1);
            var result = await this.PostAsync(Endpoint.UPDATE_ORIGIN, new {Origins= request});
            return result;
        }
    }
}