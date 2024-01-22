using BlazorHtmx;
using BlazorHtmx.Components;
using BlazorHtmx.Components.Antiforgery;
using BlazorHtmx.Components.Configuration;
using BlazorHtmx.Components.Layout;
using BlazorHtmx.Components.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddSingleton<HtmxCounter.HtmxCounterState>();

builder.AddHtmx(config =>
{
	config.HistoryCacheSize = 0;
	config.IndicatorClass = "htmx-indicator";
	config.ScrollBehavior = ScrollBehavior.Auto;
	config.RefreshOnHistoryMiss = true;
	config.SelfRequestsOnly = true;
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

app.UseStaticFiles();
app.UseAntiforgery();
app.UseMiddleware<HtmxAntiforgeryMiddleware>();

app.MapRazorComponents<HtmxApp<AppLayout>>();

app.MapGet("/love-htmx",
    () => new RazorComponentResult<LoveHtmx>(new { Message = "I ❤️ ASP.NET Core" }));

app.MapGet("/stream-mood", () => new RazorComponentResult<MoodLoader>()
{
	PreventStreamingRendering = false
});

app.MapPost("/count",
    (HtmxCounter.HtmxCounterState value) =>
    {
        value.Value++;
        return new RazorComponentResult<HtmxCounter>(
            new { State = value }
        );
    });


app.Run();