namespace Presentation.Product.Domain.Products
{
    public class ProductViewResponseModel
    {
        public IEnumerable<ProductView> Products { get; set; }
        public int TotalProduct { get; set; }
    }
}