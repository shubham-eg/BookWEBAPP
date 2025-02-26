using BookManagementFrontend;
using BookManagementFrontend.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

// Corrected HttpClient configuration
builder.Services.AddScoped(sp => new HttpClient
{
	BaseAddress = new Uri("https://localhost:7193/") // Backend API root URL
});

// Add MudBlazor services
builder.Services.AddMudServices();

// Register BookService
builder.Services.AddScoped<BookService>();

await builder.Build().RunAsync();