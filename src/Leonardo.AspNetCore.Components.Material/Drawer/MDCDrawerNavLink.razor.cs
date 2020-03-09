using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Leonardo.AspNetCore.Components.Material.Drawer
{
    public partial class MDCDrawerNavLink
    {
        [CascadingParameter] public MDCDrawer Drawer { get; set; }

        [Parameter] public string Text { get; set; }

        [Parameter] public string Icon { get; set; }

        [Parameter] public NavLinkMatch Match { get; set; } = NavLinkMatch.All;

        [Parameter] public string Href { get;set; }
    }
}
