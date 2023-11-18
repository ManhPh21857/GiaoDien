namespace Presentation.Product.Domain.Products
{
  public class ProductItem
  {
    public int? Id { get; set; }
    public string? Name { get; set; }
    public float? MinPrice { get; set; }
    public float? MaxPrice { get; set; }
    public int? Quantity { get; set; }
    public string? Image { get; set; }
    public string? Description { get; set; }
  }
}