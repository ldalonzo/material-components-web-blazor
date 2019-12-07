using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Leonardo.AspNetCore.Components.Material.Drawer
{
    /// <summary>
    /// The MDC Navigation Drawer is used to organize access to destinations and other functionality on an app.
    /// </summary>
    /// <see href="https://material.io/develop/web/components/drawers/"/>
    public partial class MDCDrawer : MaterialComponent
    {
        private const string MDCDrawerComponent_AttachTo = "MDCDrawerComponent.attachTo";
        private const string MDCDrawerComponent_ToggleOpen = "MDCDrawerComponent.toggleOpen";

        [Parameter] public RenderFragment ChildContent { get; set; }

        [Inject] protected IJSRuntime JSRuntime { get; set; }

        protected ElementReference _MDCDrawer;

        protected override string BuildClassString()
        {
            return "mdc-drawer";
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync(MDCDrawerComponent_AttachTo, _MDCDrawer);
            }
        }

        public async Task ToggleOpen()
            => await JSRuntime.InvokeVoidAsync(MDCDrawerComponent_ToggleOpen, _MDCDrawer);

        public static class CSSClasses
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
