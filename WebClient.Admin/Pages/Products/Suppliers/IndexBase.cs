using Microsoft.AspNetCore.Components;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Suppliers;
using WebClient.Admin.Shared.Alert;

namespace WebClient.Admin.Pages.Products.Suppliers
{
    public class IndexBase : ComponentBase
    {
        [Inject] public ISupplierService SupplierService { get; set; }

        public PopUp PopUp { get; set; }
        public List<SupplierModel> Suppliers = new();

        protected async override Task<Task> OnInitializedAsync()
        {
            await this.GetSuppliers();

            return base.OnInitializedAsync();
        }

        public async Task GetSuppliers()
        {
            var result = await this.SupplierService.GetSupplier();

            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<SupplierResponseModel>().Data;

                var temp = response?.Suppliers ?? new();
                temp.AddRange(Suppliers.Where(x => string.IsNullOrEmpty(x.DataVersion)));

                Suppliers = temp;
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
            }
        }

        public async Task AddSupplier()
        {
            Suppliers.Add(new());
            this.StateHasChanged();
        }

        public async Task DeleteSupplier(int? id)
        {
            var param = Suppliers.Find(x => x.Id == id);

            if (param is null)
            {
                await PopUp.Error("Opp!!", "Somethings went wrong");
            }
            else
            {
                if (string.IsNullOrEmpty(param.DataVersion))
                {
                    Suppliers.Remove(param);
                    this.StateHasChanged();
                }
                else
                {
                    var result = await this.SupplierService.DeleteSupplier(param);

                    if (result.IsSuccessStatusCode)
                    {
                        await PopUp.Success();
                        await this.GetSuppliers();
                    }
                    else
                    {
                        var error = result.ConvertResponse<ErrorModel>().Data;

                        await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
                    }
                }
            }
        }

        public async Task ReactiveSupplier(int? id)
        {
            var param = Suppliers.Find(x => x.Id == id);
            var result = await this.SupplierService.ReactiveSupplier(param);

            if (result.IsSuccessStatusCode)
            {
                await PopUp.Success();
                await this.OnInitializedAsync();
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
            }
        }

        public async Task UpdateSupplier()
        {
            var result = await this.SupplierService.UpdateSupplier(Suppliers);

            if (result.IsSuccessStatusCode)
            {
                Suppliers.Clear();
                await PopUp.Success();
                await this.GetSuppliers();
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
            }
        }
    }
}
