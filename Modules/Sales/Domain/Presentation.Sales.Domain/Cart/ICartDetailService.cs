namespace Presentation.Sales.Domain.Cart
{
    public interface ICartDetailService
    {
        Task<HttpResponseMessage> GetCartdetail();
        Task<HttpResponseMessage> GetProductdetailId(int productid, int colorid, int sizeid);
        Task<HttpResponseMessage> CreateCartdetail(CartDetailModel cartDetails);
        Task<HttpResponseMessage> UpdateCartdetail(CartDetailModel cartDetails);
        Task<HttpResponseMessage> DeleteCartdetail(CartDetailModel cartDetails);
    }
}
