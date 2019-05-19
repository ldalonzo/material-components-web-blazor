using Microsoft.AspNetCore.Components;
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
            ClassString = "mdc-drawer mdc-drawer--dismissible mdc-drawer--open";
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
    }
}
