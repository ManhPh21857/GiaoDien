namespace Presentation.Product.Domain.Products
{
    public interface IProductService
    {
        Task<HttpResponseMessage> GetProductView(int pageNo, ProductFilter filter);
        Task<HttpResponseMessage> GetProductItem(int id);
        Task<HttpResponseMessage> GetProducts(int pageNo);
        Task<HttpResponseMessage> GetProduct(int id);
        Task<HttpResponseMessage> CreateProduct(ProductModel request);
        Task<HttpResponseMessage> DeleteProduct(int id);
    }
}