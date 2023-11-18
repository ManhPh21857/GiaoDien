namespace Presentation.Product.Domain.Products
{
    public class ProductModel
    {
        public Product Product { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public List<ProductDetail> ProductDetails { get; set; }

        public ProductModel()
        {
            Product = new();
            ProductColors = new();
            ProductSizes = new();
            ProductDetails = new();
        }
    }
}