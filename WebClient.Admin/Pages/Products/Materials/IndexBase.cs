using Microsoft.AspNetCore.Components;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Materials;
using WebClient.Admin.Shared.Alert;

namespace WebClient.Admin.Pages.Products.Materials
{
    public class IndexBase : ComponentBase
    {
        [Inject] public IMaterialService materialService { get; set; }

        public PopUp PopUp { get; set; }
        public List<MaterialModel> Materials = new();

        protected async override Task<Task> OnInitializedAsync()
        {
            await this.GetMaterials();

            return base.OnInitializedAsync();
        }

        public async Task GetMaterials()
        {
            var result = await this.materialService.GetMaterial();

            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<MaterialsResponseModel>().Data;

                var temp = response?.Materials ?? new();
                temp.AddRange(Materials.Where(x => string.IsNullOrEmpty(x.DataVersion)));

                Materials = temp;
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
            }
        }

        public async Task AddMaterial()
        {
            Materials.Add(new());
            this.StateHasChanged();
        }

        public async Task DeleteMaterial(int? id)
        {
            var param = Materials.Find(x => x.Id == id);

            if (param is null)
            {
                await PopUp.Error("Opp!!", "Somethings went wrong");
            }
            else
            {
                if (string.IsNullOrEmpty(param.DataVersion))
                {
                    Materials.Remove(param);
                    this.StateHasChanged();
                }
                else
                {
                    var result = await this.materialService.DeleteMaterial(param);

                    if (result.IsSuccessStatusCode)
                    {
                        await PopUp.Success();
                        await this.GetMaterials();
                    }
                    else
                    {
                        var error = result.ConvertResponse<ErrorModel>().Data;

                        await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
                    }
                }
            }
        }

        public async Task ReactiveMaterial(int? id)
        {
            var param = Materials.Find(x => x.Id == id);
            var result = await this.materialService.ReactiveMaterial(param);

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

        public async Task UpdateMaterial()
        {
            var result = await this.materialService.UpdateMaterial(Materials);

            if (result.IsSuccessStatusCode)
            {
                Materials.Clear();
                await PopUp.Success();
                await this.GetMaterials();
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
            }
        }
    }
}
