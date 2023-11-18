using Presentation.Product.Domain.Products;

namespace Presentation.Sales.Domain.Cart
{
    public class CartDetailModel
    {
        public int? CartId { get; set; }
        public int? ProductDetailId { get; set; }
        public string Image { get; set; }
        public string ProductName { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public float? Price { get; set; }
        public int? Quantity { get; set; }
        public string DataVersion { get; set; }
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }

        public bool Adoption { get; set; }
    }
}
