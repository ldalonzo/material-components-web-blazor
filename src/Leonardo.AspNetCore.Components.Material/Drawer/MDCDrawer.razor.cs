using Leonardo.AspNetCore.Components.Material.List;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;
using System.Threading.Tasks;

namespace Leonardo.AspNetCore.Components.Material.Drawer
{
    /// <summary>
    /// The MDC Navigation Drawer is used to organize access to destinations and other functionality on an app.
    /// </summary>
    /// <see href="https://material.io/develop/web/components/drawers/"/>
    public partial class MDCDrawer : MaterialComponent
    {
        [Parameter] public string Title { get; set; }

        [Parameter] public string Subtitle { get; set; }

        [Parameter] public RenderFragment DrawerContent { get; set; }

        [Parameter] public MDCDrawerVariant Variant { get; set; }

        [Inject] protected IJSRuntime JSRuntime { get; set; }

        protected ElementReference _MDCDrawer;

        protected ElementReference _MDCList;

        public string MDCListId => _MDCList.Id;

        protected override string BuildClassString()
        {
            var sb = new StringBuilder(CSSClasses.MDCDrawer);

            if (Variant == MDCDrawerVariant.Dismissible)
            {
                sb.Append(" ");
                sb.Append(CSSClasses.MDCDrawerDismissible);

                sb.Append(" ");
                sb.Append(CSSClasses.MDCDrawerOpen);
            }

            if (!string.IsNullOrWhiteSpace(Class))
            {
                sb.Append($" {Class}");
            }

            return sb.ToString();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                // For permanently visible drawer, the list must be instantiated for appropriate keyboard interaction.
                await MDCListJSRuntime.AttachTo(JSRuntime, _MDCList, true);
            }
        }

        private static class CSSClasses
        {
            public const string MDCDrawer = "mdc-drawer";

            /// <summary>Dismissible drawer variant class.</summary>
            public const string MDCDrawerDismissible = "mdc-drawer--dismissible";

            /// <summary>If present, indicates that the dismissible drawer is in the open position.</summary>
            public const string MDCDrawerOpen = "mdc-drawer--open";

            /// <summary>
            /// Mandatory for dismissible variant only. Sibling element that is resized when the drawer opens/closes.
            /// </summary>
            public const string MDCDrawerAppContent = "mdc-drawer-app-content";
        }
    }
}
