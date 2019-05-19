using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Material.TopAppBar
{
    /// <summary>
    /// MDC Top App Bar acts as a container for items such as application title, navigation icon, and action items.
    /// </summary>
    /// <seealso href="https://material.io/develop/web/components/top-app-bar/"/>
    public class MDCTopAppBarComponent : BlazorMaterialComponent
    {
        private const string MDCTopAppBarComponent_AttachTo = "MDCTopAppBarComponent.attachTo";
        private const string MDCTopAppBarComponent_ListenToNav = "MDCTopAppBarComponent.listenToNav";
        private const string MDCTopAppBarComponent_SetScrollTarget = "MDCTopAppBarComponent.SetScrollTarget";

        [Parameter] protected RenderFragment ChildContent { get; set; }

        [Parameter] protected Func<Task> OnNav { get; set; }

        protected string ClassString { get; set; }

        protected ElementRef _MDCTopAppBar;
        private bool _isFirstRender = true;

        protected override void OnInit()
        {
            var sb = new StringBuilder(CSSClasses.MDCTopAppBar);

            ClassString = sb.ToString();
        }

        protected override async Task OnAfterRenderAsync()
        {
            if (_isFirstRender)
            {
                _isFirstRender = false;

                await JSRuntime.InvokeAsync<bool>(MDCTopAppBarComponent_AttachTo, _MDCTopAppBar);
                await JSRuntime.InvokeAsync<bool>(MDCTopAppBarComponent_ListenToNav, new DotNetObjectRef(this));
            }
        }

        /// <summary>
        /// Sets scroll target to different DOM node (default is window).
        /// </summary>
        public Task SetScrollTarget(ElementRef target) =>
            JSRuntime.InvokeAsync<bool>(MDCTopAppBarComponent_SetScrollTarget, _MDCTopAppBar, target);

        /// <summary>
        /// Emits when the navigation icon is clicked.
        /// </summary>
        [JSInvokable]
        public Task OnMDCTopAppBarNav() => OnNav != null ? OnNav() : Task.CompletedTask;

        public static class CSSClasses
        {
            internal const string MDCTopAppBar = "mdc-top-app-bar";

            /// <summary>
            /// Class used to style the content below the standard and fixed top app bar to prevent the top app bar from covering it.
            /// </summary>
            public const string MDCTopAppBarFixedAdjust = "mdc-top-app-bar--fixed-adjust";
        }
    }
}
