using Microsoft.AspNetCore.Components;

namespace Blazor.Material.Components.WebApp.Shared
{
    public partial class ComponentCatalogPanel : ComponentBase
    {
        [Parameter] public string Title { get; set; }

        [Parameter] public RenderFragment HeroComponent { get; set; }

        [Parameter] public RenderFragment HeroComponentOptions { get; set; }

        [Parameter] public RenderFragment Demos { get; set; }
    }
}
