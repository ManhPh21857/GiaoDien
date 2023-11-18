using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Suppliers;

namespace WebClient.Admin.Pages.Products.Modal
{
    public partial class CreateSupplierModal : ComponentBase
    {
        [Parameter] public Action Success { get; set; }
        [Parameter] public Action<string?, string?> Error { get; set; }
        [Parameter] public Func<Task> GetSupplier { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ISupplierService SupplierService { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }

        private SupplierModel supplier = new();
        private readonly string modalId = "createSupplierModal";
        private readonly string inputNameId = "supplierNameInput";
        private readonly string inputAddressId = "supplierAddressInput";
        private string inputNameValidate;
        private string inputAddressValidate;

        private async Task Create()
        {
            if (!supplier.Name.Any())
            {
                this.inputNameValidate = "is-invalid";
                await this.JsRuntime.InvokeVoidAsync("focusInput", inputNameId);
                return;
            }

            if (!supplier.Address.Any())
            {
                this.inputAddressValidate = "is-invalid";
                await this.JsRuntime.InvokeVoidAsync("focusInput", inputAddressId);
                return;
            }

            var result = await this.SupplierService.UpdateSupplier(new List<SupplierModel> { supplier });

            if (result.IsSuccessStatusCode)
            {
                await this.JsRuntime.InvokeVoidAsync("closeModal", modalId + "close");
                this.Success();
                await this.GetSupplier();
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;
                this.Error(error?.ErrorCode, error?.ErrorMessage);
            }
        }

        public void GotoDetail()
        {
            this.NavigationManager.NavigateTo("product/supplier");
        }

        private void CheckNameInput()
        {
            this.inputNameValidate = this.supplier.Name.Any() ? "is-valid" : "is-invalid";
            this.StateHasChanged();
        }

        private void CheckAddressInput()
        {
            this.inputAddressValidate = this.supplier.Address.Any() ? "is-valid" : "is-invalid";
            this.StateHasChanged();
        }
    }
}