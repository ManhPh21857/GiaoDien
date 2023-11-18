using Blazored.LocalStorage;
using Presentation.Core.Service;
using Presentation.Product.Domain.Suppliers;

namespace Presentation.Product.Service.Suppliers
{
    public class SupplierService : ApiClient, ISupplierService
    {
        public SupplierService(HttpClient httpClient, ILocalStorageService localStorageService) : base(httpClient,
            localStorageService)
        {
        }

        public async Task<HttpResponseMessage> DeleteSupplier(SupplierModel supplier)
        {
            var result = await this.PutAsync(Endpoint.DELETE_SUPLIER, supplier);
            return result;
        }

        public async Task<HttpResponseMessage> GetSupplier()
        {
            var result = await this.GetAsync(Endpoint.GET_SUPLIER);

            return result;
        }

        public async Task<HttpResponseMessage> ReactiveSupplier(SupplierModel supplier)
        {
            var result = await this.PutAsync(Endpoint.REACTIVE_SUPLIER, supplier);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateSupplier(IEnumerable<SupplierModel> supplier)
        {
            var request = supplier.Where(x => x.IsDeleted != 1);
            var result = await this.PostAsync(Endpoint.UPDATE_SUPLIER,
                new { Suppliers = request });
            return result;
        }
    }
}