namespace Presentation.Product.Domain.Trademarks
{
    public interface ITrademarkService
    {
        Task<HttpResponseMessage> GetTrademark();
        Task<HttpResponseMessage> UpdateTrademark(IEnumerable<TrademarkModel> trademark);
        Task<HttpResponseMessage> DeleteTrademark(TrademarkModel trademark);
        Task<HttpResponseMessage> ReactiveTrademark(TrademarkModel trademark);
    }
}
