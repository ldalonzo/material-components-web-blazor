using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Blazor.Material.TopAppBar
{
    /// <summary>
    /// MDC Top App Bar acts as a container for items such as application title, navigation icon, and action items.
    /// </summary>
    /// <seealso href="https://material.io/develop/web/components/top-app-bar/"/>
    public class MDCTopAppBarComponent : BlazorMaterialComponent
    {
        private const string AttachTo = "MDCTopAppBarComponent.attachTo";
        private const string ListenToNav = "MDCTopAppBarComponent.listenToNav";
        private const string MDCTopAppBarComponentSetScrollTarget = "MDCTopAppBarComponent.SetScrollTarget";

        [Parameter] protected RenderFragment ChildContent { get; set; }

        [Parameter] protected Func<Task> OnNav { get; set; }

        protected string ClassString { get; set; }

        protected ElementRef _MDCTopAppBar;
        private bool _isFirstRender = true;

        protected override void OnInit()
        {
            ClassString = "mdc-top-app-bar";
        }

        protected override async Task OnAfterRenderAsync()
        {
            if (_isFirstRender)
            {
                _isFirstRender = false;

                await JSRuntime.InvokeAsync<bool>(AttachTo, _MDCTopAppBar);
                await JSRuntime.InvokeAsync<bool>(ListenToNav, new DotNetObjectRef(this));
            }
        }

        /// <summary>
        /// Sets scroll target to different DOM node (default is window).
        /// </summary>
        public Task SetScrollTarget(ElementRef target) =>
            JSRuntime.InvokeAsync<bool>(MDCTopAppBarComponentSetScrollTarget, _MDCTopAppBar, target);

        /// <summary>
        /// Emits when the navigation icon is clicked.
        /// </summary>
        [JSInvokable]
        public Task OnMDCTopAppBarNav() => OnNav != null ? OnNav() : Task.CompletedTask;
    }
}
