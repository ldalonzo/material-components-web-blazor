using Blazor.Material.Components;
using Microsoft.AspNetCore.Components;
using System.Text;

namespace Blazor.Material.Drawer
{
    public class MDCDrawerAppContentComponent : MaterialComponent
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected string ClassString { get; private set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var sb = new StringBuilder(MDCDrawerComponent.CSSClasses.MDCDrawerAppContent);

            if (!string.IsNullOrWhiteSpace(Class))
            {
                sb.Append($" {Class}");
            }

            ClassString = sb.ToString();
        }
    }
}
