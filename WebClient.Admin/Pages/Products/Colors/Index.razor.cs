using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Colors;
using WebClient.Admin.Shared.Alert;

#pragma warning disable CS8618

namespace WebClient.Admin.Pages.Products.Colors
{
    public partial class Index : ComponentBase
    {
        [Inject] private IColorService ColorService { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; }

        private PopUp PopUp { get; set; }
        private List<ColorModel> colors = new();

        protected async override Task<Task> OnInitializedAsync()
        {
            await this.GetColor();

            return base.OnInitializedAsync();
        }

        private async Task GetColor()
        {
            var result = await this.ColorService.GetColors();

            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<ColorResponseModel>().Data;

                var temp = response?.Colors ?? new();
                temp.AddRange(colors.Where(x => string.IsNullOrEmpty(x.DataVersion)));

                colors = temp;
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await this.PopUp.Error(error?.ErrorCode, error?.ErrorMessage);
            }
        }

        private void AddColor()
        {
            this.colors.Add(new());
            this.StateHasChanged();
        }

        private async Task DeleteColor(int? id)
        {
            var param = this.colors.Find(x => x.Id == id);

            if (param is null)
            {
                await this.PopUp.Error("Opp!!", "Somethings went wrong");
            }
            else
            {
                if (string.IsNullOrEmpty(param.DataVersion))
                {
                    this.colors.Remove(param);
                    this.StateHasChanged();
                }
                else
                {
                    var result = await this.ColorService.DeleteColor(param);

                    if (result.IsSuccessStatusCode)
                    {
                        await this.PopUp.Success();
                        await this.GetColor();
                    }
                    else
                    {
                        var error = result.ConvertResponse<ErrorModel>().Data;

                        await this.PopUp.Error(error?.ErrorCode, error?.ErrorMessage);
                    }
                }
            }
        }

        private async Task ReactiveColor(int? id)
        {
            var param = this.colors.Find(x => x.Id == id);
            if (param is null)
            {
                await this.PopUp.Error("Opp!!", "Somethings went wrong");
            }
            else
            {
                if (string.IsNullOrEmpty(param.DataVersion))
                {
                    this.colors.Remove(param);
                    this.StateHasChanged();
                }
                else
                {
                    var result = await this.ColorService.ReactiveColor(param);

                    if (result.IsSuccessStatusCode)
                    {
                        await this.PopUp.Success();
                        await this.GetColor();
                    }
                    else
                    {
                        var error = result.ConvertResponse<ErrorModel>().Data;

                        await this.PopUp.Error(error?.ErrorCode, error?.ErrorMessage);
                    }
                }
            }
        }

        private async Task UpdateColor()
        {
            this.colors.ForEach(this.Validator);

            var result = await this.ColorService.UpdateColor(this.colors);

            if (result.IsSuccessStatusCode)
            {
                await this.PopUp.Success();
                this.colors.Clear();
                await this.GetColor();
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await this.PopUp.Error(error?.ErrorCode, error?.ErrorMessage);
            }
        }

        private async void Validator(ColorModel color)
        {
            if (colors.Count(x => x.Color == color.Color) > 1 || string.IsNullOrEmpty(color.Color))
            {
                await this.PopUp.Error("Opp!!", "invalid value");
                await this.JsRuntime.InvokeVoidAsync("focusInput", this.colors.IndexOf(color) + 1);
            }
        }
    }
}