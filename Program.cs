using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TestBlazorApp;
using TestBlazorApp.Servicios;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


var apiURL = builder.Configuration.GetValue<string>("API-URL");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiURL) });


builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();




await builder.Build().RunAsync();
