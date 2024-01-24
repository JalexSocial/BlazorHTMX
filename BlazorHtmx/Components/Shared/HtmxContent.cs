using System.Xml;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorHtmx.Components.Shared
{
    public class HtmxContent : ComponentBase
    {
        private Dictionary<string, object> _attributes = new Dictionary<string, object>();

        [CascadingParameter]
        public HtmxRequestView? Parent { get; set; }

        [Parameter] 
        public string Id { get; set; } = default!;

        [Parameter] 
        public RenderFragment ChildContent { get; set; } = default!;

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.AddContent(0, ChildContent);
        }

        /// <summary>
        /// Register content with parent component
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        protected override void OnInitialized()
        {
            if (Parent is null)
                throw new ArgumentNullException(nameof(Parent), "HtmxContent must be placed inside an HtmxRequestView");
            
            Parent.AddContent(this);
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
        }
    }
}
