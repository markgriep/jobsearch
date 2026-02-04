using MudBlazor.Services;
using jobsearch.Components;
using jobsearch.Data;
using jobsearch.Interfaces;
using jobsearch.Configuration;
using Microsoft.EntityFrameworkCore;
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


var dbPath = DatabasePathResolver.Resolve(builder.Environment.ContentRootPath);

//var relativeDbPath = builder.Configuration["DatabasePath"] ?? "db/Starter-JobSearch.db";

//var dbPath = Path.GetFullPath(
//    Path.Combine(builder.Environment.ContentRootPath, relativeDbPath)
//);


//Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
builder.Services.AddDbContext<JobSearchDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath};Cache=Shared"));


var openAiApiKey = builder.Configuration["OpenAI:ApiKey"];
if (string.IsNullOrWhiteSpace(openAiApiKey))
{
    throw new InvalidOperationException("Missing OpenAI:ApiKey secret.");
}

var openAiTemperature = builder.Configuration.GetValue<double?>("OpenAI:Temperature") ?? 0.8d;

builder.Services.AddSingleton(new OpenAiSettings
{
    ApiKey = openAiApiKey,
    Temperature = openAiTemperature
});

var app = builder.Build();

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
