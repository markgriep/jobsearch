using MudBlazor.Services;
using jobsearch.Components;
using jobsearch.Data;
using jobsearch.Interfaces;
using jobsearch.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Refit;


var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add Refit client
builder.Services
    .AddHttpClient("openai", client => 
    {
        client.BaseAddress = new Uri("https://api.openai.com");
    });

builder.Services.AddScoped<IOpenAIClient>(sp =>
    RestService.For<IOpenAIClient>(
        sp.GetRequiredService<IHttpClientFactory>().CreateClient("openai")));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


//var dbPath = DatabasePathResolver.Resolve(builder.Environment.ContentRootPath);

////var relativeDbPath = builder.Configuration["DatabasePath"] ?? "db/Starter-JobSearch.db";

////var dbPath = Path.GetFullPath(
////    Path.Combine(builder.Environment.ContentRootPath, relativeDbPath)
////);


////Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
//builder.Services.AddDbContext<JobSearchDbContext>(options =>
//    options.UseSqlite($"Data Source={dbPath};Cache=Shared"));


var connectionString =
    builder.Configuration.GetConnectionString("JobSearch");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string 'JobSearch' is not configured.");
}

builder.Services.AddDbContext<JobSearchDbContext>(options =>
    options.UseSqlServer(connectionString));





var openAiSettings = new OpenAiSettings
{
    ApiKey = builder.Configuration["OpenAI:ApiKey"]?.Trim() ?? string.Empty,
    Temperature = builder.Configuration.GetValue<double?>("OpenAI:Temperature") ?? 0.8d
};

builder.Services.AddSingleton(openAiSettings);

var app = builder.Build();

if (string.IsNullOrWhiteSpace(openAiSettings.ApiKey))
{
    app.Logger.LogWarning("OpenAI:ApiKey is not configured. The OpenAI page will require entering a key manually.");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
