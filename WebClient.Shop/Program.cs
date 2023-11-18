using Blazored.LocalStorage;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Presentation.Core.Service;
using WebClient.Shop;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddCoreService();
builder.Services.AddWebClient();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5001/") });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
