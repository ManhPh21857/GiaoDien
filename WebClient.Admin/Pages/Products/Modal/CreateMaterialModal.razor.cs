using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Materials;

namespace WebClient.Admin.Pages.Products.Modal
{
    public partial class CreateMaterialModal : ComponentBase
    {
        [Parameter] public Action Success { get; set; }
        [Parameter] public Action<string?, string?> Error { get; set; }
        [Parameter] public Func<Task> GetMaterial { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IMaterialService MaterialService { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }

        private string materialName = "";
        private readonly string modalId = "createMaterialModal";
        private readonly string inputId = "materialInput";
        private string inputValidate;

        private async Task Create()
        {
            if (materialName.Any())
            {
                var result = await MaterialService.UpdateMaterial(new List<MaterialModel> { new() { Name = materialName } });

                if (result.IsSuccessStatusCode)
                {
                    await this.JsRuntime.InvokeVoidAsync("closeModal", modalId + "close");
                    this.Success();
                    await this.GetMaterial();
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
            this.NavigationManager.NavigateTo("product/material");
        }

        private void CheckInput()
        {
            this.inputValidate = this.materialName.Any() ? "is-valid" : "is-invalid";
            this.StateHasChanged();
        }
    }
}