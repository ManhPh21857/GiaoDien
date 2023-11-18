namespace Presentation.Product.Domain.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public int? ClassificationId { get; set; }
        public int? MaterialId { get; set; }
        public int? SupplierId { get; set; }
        public int? TrademarkId { get; set; }
        public int? OriginId { get; set; }
        public string Description { get; set; }
        public string DataVersion { get; set; }
    }
}
