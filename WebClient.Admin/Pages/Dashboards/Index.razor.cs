using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace WebClient.Admin.Pages.Dashboards
{
    public partial class Index : ComponentBase
    {
        [Inject] private ILocalStorageService LocalStorageService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }

        protected async override Task<Task> OnInitializedAsync()
        {
            var token = await this.LocalStorageService.GetItemAsStringAsync("token");

            if (string.IsNullOrEmpty(token))
            {
                this.NavigationManager.NavigateTo("login");
            }

            return base.OnInitializedAsync();
        }
    }
}
