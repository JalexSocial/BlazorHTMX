using Htmx;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorHtmx.Components.Layout;

public class HtmxApp<T> : ComponentBase where T : LayoutComponentBase
{
	[CascadingParameter]
	public HttpContext? Context { get; set; }

	protected override void BuildRenderTree(RenderTreeBuilder builder)
	{
		builder.OpenComponent<HtmxLayout<T>>(0);
		builder.AddAttribute(1, "Body", (RenderFragment)(builder2 =>
		{
			builder2.OpenComponent<Routes>(2);
			builder2.CloseComponent();
		}));
		builder.CloseComponent();
	}
}
