namespace Presentation.Product.Domain.Suppliers
{
    public class SupplierModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? IsDeleted { get; set; }
        public string DataVersion { get; set; }
    }
}
