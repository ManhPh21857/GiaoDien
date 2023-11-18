using Microsoft.AspNetCore.Components;

namespace WebClient.Shop.Pages.Home
{
  public partial class Content : ComponentBase
  {
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    private void NavigateTo()
    {
      NavigationManager.NavigateTo("product/1");
    }
  }
}
