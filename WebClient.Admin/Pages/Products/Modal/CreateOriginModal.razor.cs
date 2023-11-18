using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Origins;

namespace WebClient.Admin.Pages.Products.Modal
{
    public partial class CreateOriginModal : ComponentBase
    {
        [Parameter] public Action Success { get; set; }
        [Parameter] public Action<string?, string?> Error { get; set; }
        [Parameter] public Func<Task> GetOrigin { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IOriginService OriginService { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }

        private string originName = "";
        private readonly string modalId = "createOriginModal";
        private readonly string inputId = "originNameInput";
        private string inputValidate;

        private async Task Create()
        {
            if (originName.Any())
            {
                var result = await this.OriginService.UpdateOrigin(new List<OriginModel> { new() { Name = originName } });

                if (result.IsSuccessStatusCode)
                {
                    await this.JsRuntime.InvokeVoidAsync("closeModal", modalId + "close");
                    this.Success();
                    await this.GetOrigin();
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

        public void GotoDetail()
        {
            this.NavigationManager.NavigateTo("product/origin");
        }

        private void CheckInput()
        {
            this.inputValidate = this.originName.Any() ? "is-valid" : "is-invalid";
            this.StateHasChanged();
        }
    }
}