namespace Presentation.Product.Domain.Products
{
    public class ProductDetail
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }
        public float? ImportPrice { get; set; }
        public float? Price { get; set; }
        public float? Quantity { get; set; }
        public string? DataVersion { get; set; }
        public string? Image { get; set; }
    }
}
