using Blazored.LocalStorage;
using CustompgAdmin.UI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddBlazoredLocalStorageAsSingleton();
builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7141") });

await builder.Build().RunAsync();
