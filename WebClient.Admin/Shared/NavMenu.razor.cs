using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace WebClient.Admin.Shared
{
    public partial class NavMenu : ComponentBase
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private ILocalStorageService LocalStorageService { get; set; }

        protected async override Task<Task> OnInitializedAsync()
        {
            var token = await this.LocalStorageService.GetItemAsStringAsync("token");

            if (string.IsNullOrEmpty(token))
            {
                this.NavigationManager.NavigateTo("login");
                return base.OnInitializedAsync();
            }

            await this.GetUser();

            return base.OnInitializedAsync();
        }

        public async Task GetUser()
        {

        }
    }
}
