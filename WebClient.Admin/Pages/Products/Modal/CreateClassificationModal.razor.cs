using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Classifications;
#pragma warning disable CS8618

namespace WebClient.Admin.Pages.Products.Modal
{
    public partial class CreateClassificationModal : ComponentBase
    {
        [Parameter] public Action Success { get; set; }
        [Parameter] public Action<string?, string?> Error { get; set; }
        [Parameter] public Func<Task> GetClassification { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IClassificationService ClassificationService { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }

        private string classificationName = "";
        private readonly string modalId = "createClassificationModal";
        private readonly string inputId = "classificationNameInput";
        private string inputValidate = "";

        private async Task Create()
        {
            if (classificationName.Any())
            {
                var result = await ClassificationService.UpdateClassfication(
                    new List<ClassificationModel>
                    {
                        new()
                        {
                            Name = classificationName
                        }
                    }
                );

                if (result.IsSuccessStatusCode)
                {
                    await this.JsRuntime.InvokeVoidAsync("closeModal", modalId + "close");
                    this.Success();
                    await this.GetClassification();
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
            this.NavigationManager.NavigateTo("product/classification");
        }

        private void CheckInput()
        {
            this.inputValidate = this.classificationName.Any() ? "is-valid" : "is-invalid";
            this.StateHasChanged();
        }
    }
}