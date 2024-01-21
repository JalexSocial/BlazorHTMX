using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.HtmlRendering;

namespace BlazorHtmx.Helpers;

// Credits to Egil Hansen
public static class HtmlRendererExtensions
{
	public static Task<HtmlRootComponent> RenderAsync(this HtmlRenderer renderer, RenderFragment renderFragment)
	{
		var dictionary = new Dictionary<string, object?>
		{
			{ "ChildContent", renderFragment }
		};
		var parameters = ParameterView.FromDictionary(dictionary);
		return renderer.RenderComponentAsync<FragmentContainer>(parameters);
	}

	private sealed class FragmentContainer : IComponent
	{
		private RenderHandle? _renderHandle;

		public void Attach(RenderHandle renderHandle) => this._renderHandle = renderHandle;

		public Task SetParametersAsync(ParameterView parameters)
		{
			if (parameters.TryGetValue<RenderFragment>("ChildContent", out var childContent))
			{
				_renderHandle?.Render(childContent);
			}
			return Task.CompletedTask;
		}
	}
}
