using Htmx;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorHtmx.Components.Layout;

public class HtmxLayout<T> : LayoutComponentBase where T : LayoutComponentBase
{
	internal class BaseLayout : LayoutComponentBase
	{
		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			builder.OpenElement(0, "html");
			builder.OpenElement(1, "body");
			builder.AddContent(2, Body);
			builder.CloseElement();
			builder.CloseElement();
        }
    }

	[CascadingParameter]
	public HttpContext? Context { get; set; }

	protected override void BuildRenderTree(RenderTreeBuilder builder)
	{
		if (Context?.Request?.IsHtmx() == true)
		{
			Context?.Response.Headers.TryAdd("Vary", Htmx.HtmxRequestHeaders.Keys.Request);
			builder.OpenComponent<BaseLayout>(0);
		}
		else
		{
			builder.OpenComponent<T>(0);
		}

		builder.AddComponentParameter(1, "Body", Body);
		builder.CloseComponent();
	}
}
