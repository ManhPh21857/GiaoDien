namespace Presentation.Product.Domain.Suppliers
{
    public interface ISupplierService
    {
        //Task<List<SupplierModel>> GetSuppliers();
        Task<HttpResponseMessage> GetSupplier();
        Task<HttpResponseMessage> UpdateSupplier(IEnumerable<SupplierModel> supplier);
        Task<HttpResponseMessage> DeleteSupplier(SupplierModel supplier);
        Task<HttpResponseMessage> ReactiveSupplier(SupplierModel supplier);

    }
}
