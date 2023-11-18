using Blazored.LocalStorage;
using Presentation.Core.Service;
using Presentation.Product.Domain.Colors;

namespace Presentation.Product.Service.Colors
{
    public class ColorService : ApiClient, IColorService
    {
        public ColorService(HttpClient httpClient, ILocalStorageService localStorageService) : base(httpClient, localStorageService)
        {
        }

        public async Task<HttpResponseMessage> GetColors()
        {
            var response = await this.GetAsync(Endpoint.GET_COLOR);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateColor(IEnumerable<ColorModel> param)
        {
            var request = param.Where(x => x.IsDeleted != 1);

            var response = await this.PostAsync(Endpoint.UPDATE_COLOR, new { Colors = request });

            return response;
        }

        public async Task<HttpResponseMessage> DeleteColor(ColorModel param)
        {
            var response = await this.PutAsync(Endpoint.DELETE_COLOR, param);

            return response;
        }

        public async Task<HttpResponseMessage> ReactiveColor(ColorModel param)
        {
            var response = await this.PutAsync(Endpoint.REACTIVE_COLOR, param);

            return response;
        }
    }
}