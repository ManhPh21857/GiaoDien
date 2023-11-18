using Microsoft.AspNetCore.Components;
using Presentation.Core.Domain;
using Presentation.Core.Service;
using Presentation.Product.Domain.Classifications;
using WebClient.Admin.Shared.Alert;

namespace WebClient.Admin.Pages.Products.Classificatons
{
    public class IndexBase : ComponentBase
    {
        [Inject] public IClassificationService classficationService { get; set; }

        public PopUp PopUp { get; set; }
        public List<ClassificationModel> Classifications = new();

        protected async override Task<Task> OnInitializedAsync()
        {
            await this.GetClassifications();

            return base.OnInitializedAsync();
        }

        public async Task GetClassifications()
        {
            var result = await this.classficationService.GetClassfication();

            if (result.IsSuccessStatusCode)
            {
                var response = result.ConvertResponse<ClassficationResponseModel>().Data;

                var temp = response?.Classifications ?? new();
                temp.AddRange(Classifications.Where(x => string.IsNullOrEmpty(x.DataVersion)));

                Classifications = temp;
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
            }
        }

        public async Task AddClassification()
        {
            Classifications.Add(new());
            this.StateHasChanged();
        }

        public async Task DeleteClassification(int? id)
        {
            var param = Classifications.Find(x => x.Id == id);

            if (param is null)
            {
                await PopUp.Error("Opp!!", "Somethings went wrong");
            }
            else
            {
                if (string.IsNullOrEmpty(param.DataVersion))
                {
                    Classifications.Remove(param);
                    this.StateHasChanged();
                }
                else
                {
                    var result = await this.classficationService.DeleteClassfication(param);

                    if (result.IsSuccessStatusCode)
                    {
                        await PopUp.Success();
                        await this.GetClassifications();
                    }
                    else
                    {
                        var error = result.ConvertResponse<ErrorModel>().Data;

                        await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
                    }
                }
            }
        }

        public async Task ReactiveClassification(int? id)
        {
            var param = Classifications.Find(x => x.Id == id);
            var result = await this.classficationService.ReactiveClassfication(param);

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

        public async Task UpdateClassification()
        {
            var result = await this.classficationService.UpdateClassfication(Classifications);

            if (result.IsSuccessStatusCode)
            {
                Classifications.Clear();
                await PopUp.Success();
                await this.GetClassifications();
            }
            else
            {
                var error = result.ConvertResponse<ErrorModel>().Data;

                await PopUp.Error(error?.ErrorCode ?? "", error?.ErrorMessage ?? "");
            }
        }
    }
}
