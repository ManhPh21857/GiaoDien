namespace Presentation.Product.Domain.Products
{
    public class ProductView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public float MinPrice { get; set; }
        public float MaxPrice { get; set; }
        public int Quantity { get; set; }
    }
}
