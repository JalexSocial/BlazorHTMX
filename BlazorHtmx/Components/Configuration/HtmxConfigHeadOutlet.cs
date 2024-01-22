using System.Text.Json;
using Microsoft.AspNetCore.Components;

namespace BlazorHtmx.Components.Configuration;

public class HtmxConfigHeadOutlet : IComponent
{
    [Inject] private HtmxConfig Config { get; set; } = default!;

    public void Attach(RenderHandle renderHandle)
    {
        var json = JsonSerializer.Serialize(Config, HtmxConfig.SerializerOptions);
        renderHandle.Render(builder =>
        {
            builder.AddMarkupContent(0, @$"<meta name=""htmx-config"" content='{json}'>");
        });
    }

    public Task SetParametersAsync(ParameterView parameters) => Task.CompletedTask;
}
