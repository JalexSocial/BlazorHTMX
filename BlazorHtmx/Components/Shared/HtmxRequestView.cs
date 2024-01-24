using Htmx;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web.HtmlRendering;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorHtmx.Components.Shared;

public class HtmxRequestView : ComponentBase
{
    protected List<HtmxContent> Containers { get; } = new();

    [CascadingParameter]
    public HttpContext? Context { get; set; }

    [Parameter] 
    public RenderFragment PageContent { get; set; } = default!;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenComponent<CascadingValue<HtmxRequestView>>(0);
        builder.AddComponentParameter(1, "Value", this);
        builder.AddComponentParameter(2, "ChildContent", (RenderFragment)((builder2) =>
        {
            if (Context?.Request.IsHtmx() == true)
            {
                for (int i = 0; i < Containers.Count; i++)
                {
                    var container = Containers[i];

                    if (i == 0)
                    {
                        builder2.AddContent(4, container);
                    }
                    else
                    {
                        builder2.OpenComponent<HtmxSwapView>(5);
                        builder2.AddComponentParameter(6, "Id", container.Id);
                        builder2.AddAttribute(7, "ChildContent", (RenderFragment)((builder3) => {
                            builder3.AddContent(8, container);
                        }));
                        builder2.CloseComponent();
                    }
                }
            }
            else
            {
                builder2.AddContent(9, PageContent);
            }
        }));
        builder.CloseComponent();

    }

    internal void AddContent(HtmxContent content)
    {
        Containers.Add(content);
    }
}


