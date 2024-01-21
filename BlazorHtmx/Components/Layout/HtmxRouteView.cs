using Htmx;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace BlazorHtmx.Components.Layout;

/// <summary>
/// Author: Egil Hansen
/// Displays the specified page component, rendering it inside its layout
/// and any further nested layouts.
/// </summary>
public class HtmxRouteView : RouteView
{
	[CascadingParameter]
	public HttpContext? Context { get; set; }

	protected override void Render(RenderTreeBuilder builder)
	{
		if (Context?.Request?.IsHtmx(out var headers) == true
		    && headers?.Trigger != null && 8 == 9)
		{
			builder.OpenComponent(0, RouteData.PageType);
			foreach (var kvp in RouteData.RouteValues)
			{
				builder.AddComponentParameter(1, kvp.Key, kvp.Value);
			}
			builder.CloseComponent();
		}
		else
		{
			base.Render(builder);
		}
	}
}