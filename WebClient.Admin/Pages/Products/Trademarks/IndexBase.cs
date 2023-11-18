using Microsoft.AspNetCore.Components;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Trademarks;
using WebClient.Admin.Shared.Alert;

namespace WebClient.Admin.Pages.Products.Trademarks
{
    public class IndexBase: ComponentBase
    { 
        [Inject] public ITrademarkService TrademarkService { get; set; }

        public PopUp PopUp { get; set; }
        public List<TrademarkModel> Trademarks = new();

        protected async override Task<Task> OnInitializedAsync()
        {
            await this.GetTrademarks();

            return base.OnInitializedAsync();
        }

        public async Task GetTrademarks()
        {
            var result = await this.TrademarkService.GetTrademark();

            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<TrademarkResponseModel>().Data;

                var temp = response?.Trademarks ?? new();
                temp.AddRange(Trademarks.Where(x => string.IsNullOrEmpty(x.DataVersion)));

                Trademarks = temp;
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
            }
        }

        public async Task AddTrademark()
        {
            Trademarks.Add(new());
            this.StateHasChanged();
        }

        public async Task DeleteTrademark(int? id)
        {
            var param = Trademarks.Find(x => x.Id == id);

            if (param is null)
            {
                await PopUp.Error("Opp!!", "Somethings went wrong");
            }
            else
            {
                if (string.IsNullOrEmpty(param.DataVersion))
                {
                    Trademarks.Remove(param);
                    this.StateHasChanged();
                }
                else
                {
                    var result = await this.TrademarkService.DeleteTrademark(param);

                    if (result.IsSuccessStatusCode)
                    {
                        await PopUp.Success();
                        await this.GetTrademarks();
                    }
                    else
                    {
                        var error = result.ConvertResponse<ErrorModel>().Data;

                        await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
                    }
                }
            }
        }

        public async Task ReactiveTrademark(int? id)
        {
            var param = Trademarks.Find(x => x.Id == id);
            var result = await this.TrademarkService.ReactiveTrademark(param);

            if (result.IsSuccessStatusCode)
            {
                Trademarks.Clear();
                await PopUp.Success();
                await this.GetTrademarks();
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
            }
        }

        public async Task UpdateTrademark()
        {
            var result = await this.TrademarkService.UpdateTrademark(Trademarks);

            if (result.IsSuccessStatusCode)
            {
                Trademarks.Clear();
                await PopUp.Success();
                await this.GetTrademarks();
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
            }
        }
    }
}
