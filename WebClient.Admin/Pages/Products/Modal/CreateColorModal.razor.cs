using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Colors;
#pragma warning disable CS8618

namespace WebClient.Admin.Pages.Products.Modal
{
    public partial class CreateColorModal : ComponentBase
    {
        [Parameter] public Action Success { get; set; }
        [Parameter] public Action<string?, string?> Error { get; set; }
        [Parameter] public Func<Task> GetColor { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IColorService ColorService { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }

        private string color = "";
        private readonly string modalId = "createColorModal";
        private readonly string inputId = "colorInput";
        private string inputValidate = "";

        private async Task Create()
        {
            if (color.Any())
            {
                var result = await this.ColorService.UpdateColor(
                    new List<ColorModel>
                    {
                        new()
                        {
                            Color = color
                        }
                    }
                );

                if (result.IsSuccessStatusCode)
                {
                    await this.JsRuntime.InvokeVoidAsync("closeModal", modalId + "close");
                    this.Success();
                    await this.GetColor();
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
            this.NavigationManager.NavigateTo("product/color");
        }

        private void CheckInput()
        {
            this.inputValidate = this.color.Any() ? "is-valid" : "is-invalid";
            this.StateHasChanged();
        }
    }
}