using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Trademarks;

namespace WebClient.Admin.Pages.Products.Modal
{
    public partial class CreateTrademarkModal : ComponentBase
    {
        [Parameter] public Action Success { get; set; }
        [Parameter] public Action<string?, string?> Error { get; set; }
        [Parameter] public Func<Task> GetTrademark { get; set; }

        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ITrademarkService TrademarkService { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }

        private string trademarkName = "";
        private readonly string modalId = "createTrademarkModal";
        private readonly string inputId = "trademarkNameInput";
        private string inputValidate;

        private async Task Create()
        {
            if (trademarkName.Any())
            {
                var result = await this.TrademarkService.UpdateTrademark(new List<TrademarkModel> { new() { Name = trademarkName } });

                if (result.IsSuccessStatusCode)
                {
                    await this.JsRuntime.InvokeVoidAsync("closeModal", modalId + "close");
                    this.Success();
                    await this.GetTrademark();
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
            this.NavigationManager.NavigateTo("product/trademark");
        }

        private void CheckInput()
        {
            this.inputValidate = this.trademarkName.Any() ? "is-valid" : "is-invalid";
            this.StateHasChanged();
        }
    }
}