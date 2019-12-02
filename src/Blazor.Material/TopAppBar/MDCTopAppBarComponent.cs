using Leonardo.AspNetCore.Components.Material;
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
    public class MDCTopAppBarComponent : MaterialComponent
    {
        private const string MDCTopAppBarComponent_AttachTo = "MDCTopAppBarComponent.attachTo";
        private const string MDCTopAppBarComponent_ListenToNav = "MDCTopAppBarComponent.listenToNav";
        private const string MDCTopAppBarComponent_SetScrollTarget = "MDCTopAppBarComponent.SetScrollTarget";

        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter] public Func<Task> OnNav { get; set; }

        [Inject] private IJSRuntime JSRuntime { get; set; }

        protected string ClassString { get; set; }

        protected ElementReference _MDCTopAppBar;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            var sb = new StringBuilder(CSSClasses.MDCTopAppBar);

            ClassString = sb.ToString();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeAsync<bool>(MDCTopAppBarComponent_AttachTo, _MDCTopAppBar);
                await JSRuntime.InvokeAsync<bool>(MDCTopAppBarComponent_ListenToNav, DotNetObjectReference.Create(this));
            }
        }

        /// <summary>
        /// Sets scroll target to different DOM node (default is window).
        /// </summary>
        public async Task SetScrollTarget(ElementReference target) =>
            await JSRuntime.InvokeAsync<bool>(MDCTopAppBarComponent_SetScrollTarget, _MDCTopAppBar, target);

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
