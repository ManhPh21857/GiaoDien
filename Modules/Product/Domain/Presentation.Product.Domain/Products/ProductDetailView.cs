namespace Presentation.Product.Domain.Products
{
    public class ProductDetailView
    {
        public int? Id { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}
