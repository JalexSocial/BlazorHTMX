using BlazorHtmx.Components.Antiforgery;
using BlazorHtmx.Components.Configuration;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.Extensions.Options;

namespace BlazorHtmx;

public static class WebApplicationBuilderExtensions
{
    public static void AddHtmx(this IHostApplicationBuilder builder, Action<HtmxConfig>? configBuilder = null)
    {
        builder.Services.AddSingleton<HtmxConfig>(x =>
        {
            var config = new HtmxConfig
            {
                Antiforgery = new HtmxAntiforgeryOptions(x.GetRequiredService<IOptions<AntiforgeryOptions>>()),
            };
            configBuilder?.Invoke(config);
            return config;
        });
	}
}
