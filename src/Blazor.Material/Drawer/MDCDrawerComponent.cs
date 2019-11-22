using Blazor.Material.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Material.Drawer
{
    /// <summary>
    /// The MDC Navigation Drawer is used to organize access to destinations and other functionality on an app.
    /// </summary>
    /// <see href="https://material.io/develop/web/components/drawers/"/>
    public class MDCDrawerComponent : MaterialComponent
    {
        private const string MDCDrawerComponent_AttachTo = "MDCDrawerComponent.attachTo";
        private const string MDCDrawerComponent_ToggleOpen = "MDCDrawerComponent.toggleOpen";

        [Parameter] public RenderFragment ChildContent { get; set; }

        [Inject] protected IJSRuntime JSRuntime { get; set; }

        protected string ClassString { get; private set; }

        protected ElementReference _MDCDrawer;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var sb = new StringBuilder(CSSClasses.MDCDrawer)
                .Append($" {CSSClasses.MDCDrawerDismissible}")
                .Append($" {CSSClasses.MDCDrawerOpen}");

            if (!string.IsNullOrWhiteSpace(Class))
            {
                sb.Append($" {Class}");
            }

            ClassString = sb.ToString();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeAsync<bool>(MDCDrawerComponent_AttachTo, _MDCDrawer);
            }
        }

        public async Task ToggleOpen() => await JSRuntime.InvokeAsync<bool>(MDCDrawerComponent_ToggleOpen, _MDCDrawer);

        internal static class CSSClasses
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
