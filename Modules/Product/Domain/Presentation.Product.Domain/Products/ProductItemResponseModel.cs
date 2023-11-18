namespace Presentation.Product.Domain.Products
{
    public class ProductItemResponseModel
    {
        public ProductItem Product { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public List<ProductDetailView> ProductDetails { get; set; }
    }
}
