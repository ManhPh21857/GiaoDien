using Microsoft.AspNetCore.Components;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Origins;
using WebClient.Admin.Shared.Alert;

namespace WebClient.Admin.Pages.Products.Origins
{
    public class IndexBase : ComponentBase
    {
        [Inject] public IOriginService OriginService { get; set; }

        public PopUp PopUp { get; set; }
        public List<OriginModel> Origins = new();

        protected async override Task<Task> OnInitializedAsync()
        {
            await this.GetOrigins();

            return base.OnInitializedAsync();
        }

        public async Task GetOrigins()
        {
            var result = await this.OriginService.GetOrigin();

            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<OriginResponseModel>().Data;

                var temp = response?.Origins ?? new();
                temp.AddRange(Origins.Where(x => string.IsNullOrEmpty(x.DataVersion)));

                Origins = temp;
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
            }
        }

        public async Task AddOrigin()
        {
            Origins.Add(new());
            this.StateHasChanged();
        }

        public async Task DeleteOrigin(int? id)
        {
            var param = Origins.Find(x => x.Id == id);

            if (param is null)
            {
                await PopUp.Error("Opp!!", "Somethings went wrong");
            }
            else
            {
                if (string.IsNullOrEmpty(param.DataVersion))
                {
                    Origins.Remove(param);
                    this.StateHasChanged();
                }
                else
                {
                    var result = await this.OriginService.DeleteOrigin(param);

                    if (result.IsSuccessStatusCode)
                    {
                        await PopUp.Success();
                        await this.GetOrigins();
                    }
                    else
                    {
                        var error = result.ConvertResponse<ErrorModel>().Data;

                        await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
                    }
                }
            }
        }

        public async Task ReactiveOrigin(int? id)
        {
            var param = Origins.Find(x => x.Id == id);
            var result = await this.OriginService.ReactiveOrigin(param);

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

        public async Task UpdateOrigin()
        {
            var result = await this.OriginService.UpdateOrigin(Origins);

            if (result.IsSuccessStatusCode)
            {
                Origins.Clear();
                await PopUp.Success();
                await this.GetOrigins();
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
            }
        }
    }
}
