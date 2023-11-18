using Microsoft.AspNetCore.Components;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Colors;
using Presentation.Product.Domain.Materials;
using Presentation.Product.Domain.Sizes;
using WebClient.Admin.Shared.Alert;

namespace WebClient.Admin.Pages.Products.Sizes
{
    public class IndexBase : ComponentBase
    {
        [Inject] public ISizeService sizeService { get; set; }

        public PopUp PopUp { get; set; }
        public List<SizeModel> Sizes = new();

        protected async override Task<Task> OnInitializedAsync()
        {
            await this.GetSizes();

            return base.OnInitializedAsync();
        }

        public async Task GetSizes()
        {
            var result = await this.sizeService.GetSize();

            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<SizeResponseModel>().Data;

                var temp = response?.Sizes ?? new();
                temp.AddRange(Sizes.Where(x => string.IsNullOrEmpty(x.DataVersion)));

                Sizes = temp;
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
            }
        }

        public async Task AddSize()
        {
            Sizes.Add(new());
            this.StateHasChanged();
        }

        public async Task DeleteSize(int? id)
        {
            var param = Sizes.Find(x => x.Id == id);

            if (param is null)
            {
                await PopUp.Error("Opp!!", "Somethings went wrong");
            }
            else
            {
                if (string.IsNullOrEmpty(param.DataVersion))
                {
                    Sizes.Remove(param);
                    this.StateHasChanged();
                }
                else
                {
                    var result = await this.sizeService.DeleteSize(param);

                    if (result.IsSuccessStatusCode)
                    {
                        await PopUp.Success();
                        await this.GetSizes();
                    }
                    else
                    {
                        var error = result.ConvertResponse<ErrorModel>().Data;

                        await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
                    }
                }
            }
        }

        public async Task ReactiveSize(int? id)
        {
            var param = Sizes.Find(x => x.Id == id);
            var result = await this.sizeService.ReactiveSize(param);

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

        public async Task UpdateSize()
        {
            var result = await this.sizeService.UpdateSize(Sizes);

            if (result.IsSuccessStatusCode)
            {
                Sizes.Clear();
                await PopUp.Success();
                await this.GetSizes();
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
            }
        }
    }
}
