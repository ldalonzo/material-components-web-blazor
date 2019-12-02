using Leonardo.AspNetCore.Components.Material;
using Leonardo.AspNetCore.Components.Material.Drawer;
using Microsoft.AspNetCore.Components;
using System.Text;

namespace Blazor.Material.Drawer
{
    public class MDCDrawerAppContentComponent : MaterialComponent
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override string BuildClassString()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var sb = new StringBuilder(MDCDrawerComponent.CSSClasses.MDCDrawerAppContent);

            if (!string.IsNullOrWhiteSpace(Class))
            {
                sb.Append($" {Class}");
            }
        }
    }
}
