using Microsoft.AspNetCore.Components;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Material.Drawer
{
    /// <summary>
    /// The MDC Navigation Drawer is used to organize access to destinations and other functionality on an app.
    /// </summary>
    /// <see href="https://material.io/develop/web/components/drawers/"/>
    public class MDCDrawerComponent : BlazorMaterialComponent
    {
        private const string MDCDrawerComponent_AttachTo = "MDCDrawerComponent.attachTo";
        private const string MDCDrawerComponent_ToggleOpen = "MDCDrawerComponent.toggleOpen";

        [Parameter] protected RenderFragment ChildContent { get; set; }

        protected string ClassString { get; private set; }

        protected ElementRef _MDCDrawer;
        private bool _isFirstRender = true;

        protected override void OnInit()
        {
            var sb = new StringBuilder(CSSClasses.MDCDrawer)
                .Append($" {CSSClasses.MDCDrawerDismissible}")
                .Append($" {CSSClasses.MDCDrawerOpen}");

            if (!string.IsNullOrWhiteSpace(Class))
            {
                sb.Append($" {Class}");
            }

            ClassString = sb.ToString();
        }

        protected override async Task OnAfterRenderAsync()
        {
            if (_isFirstRender)
            {
                _isFirstRender = false;
                await JSRuntime.InvokeAsync<bool>(MDCDrawerComponent_AttachTo, _MDCDrawer);
            }
        }

        public Task ToggleOpen() => JSRuntime.InvokeAsync<bool>(MDCDrawerComponent_ToggleOpen, _MDCDrawer);

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
