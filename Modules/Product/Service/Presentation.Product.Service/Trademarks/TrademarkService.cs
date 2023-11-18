using Blazored.LocalStorage;
using Presentation.Core.Service;
using Presentation.Product.Domain.Trademarks;

namespace Presentation.Product.Service.Trademarks
{
    public class TrademarkService : ApiClient, ITrademarkService
    {
        public TrademarkService(HttpClient httpClient, ILocalStorageService localStorageService) : base(httpClient,
            localStorageService)
        {
        }

        public async Task<HttpResponseMessage> ReactiveTrademark(TrademarkModel trademark)
        {
            var result = await this.PutAsync(Endpoint.REACTIVE_TREDEMARK, trademark);
            return result;
        }

        public async Task<HttpResponseMessage> DeleteTrademark(TrademarkModel trademark)
        {
            var result = await this.PutAsync(Endpoint.DELETE_TREDEMARK, trademark);

            return result;
        }

        public async Task<HttpResponseMessage> GetTrademark()
        {
            var result = await this.GetAsync(Endpoint.GET_TREDEMARK);

            return result;
        }

        public async Task<HttpResponseMessage> UpdateTrademark(IEnumerable<TrademarkModel> trademark)
        {
            var request = trademark.Where(x => x.IsDeleted != 1);
            var result = await this.PostAsync(Endpoint.UPDATE_TREDEMARK,
                new { Trademarks = request });

            return result;
        }
    }
}