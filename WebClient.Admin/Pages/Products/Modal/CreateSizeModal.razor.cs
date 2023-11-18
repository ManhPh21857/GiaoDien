using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Sizes;

namespace WebClient.Admin.Pages.Products.Modal
{
    public partial class CreateSizeModal : ComponentBase
    {
        [Parameter] public Action Success { get; set; }
        [Parameter] public Action<string?, string?> Error { get; set; }
        [Parameter] public Func<Task> GetSize { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ISizeService SizeService { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }

        private string size = "";
        private readonly string modalId = "createSizeModal";
        private readonly string inputId = "sizeInput";
        private string inputValidate;

        private async Task Create()
        {
            if (size.Any())
            {
                var result = await this.SizeService.UpdateSize(new List<SizeModel> { new() { Size = size } });

                if (result.IsSuccessStatusCode)
                {
                    await this.JsRuntime.InvokeVoidAsync("closeModal", modalId + "close");
                    this.Success();
                    await this.GetSize();
                }
                else
                {
                    var error = result.ConvertResponse<ErrorModel>().Data;
                    this.Error(error?.ErrorCode, error?.ErrorMessage);
                }
            }
            else
            {
                this.inputValidate = "is-invalid";
                await this.JsRuntime.InvokeVoidAsync("focusInput", inputId);
            }
        }

        private void GotoDetail()
        {
            NavigationManager.NavigateTo("product/size");
        }

        private void CheckInput()
        {
            this.inputValidate = this.size.Any() ? "is-valid" : "is-invalid";
            this.StateHasChanged();
        }
    }
}