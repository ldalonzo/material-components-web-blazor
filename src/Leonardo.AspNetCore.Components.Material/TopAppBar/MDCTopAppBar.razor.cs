using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Leonardo.AspNetCore.Components.Material.TopAppBar
{
    /// <summary>
    /// MDC Top App Bar acts as a container for items such as application title, navigation icon, and action items.
    /// </summary>
    /// <seealso href="https://material.io/develop/web/components/top-app-bar/"/>
    public partial class MDCTopAppBar : MaterialComponent
    {
        [Parameter] public string Title { get; set; }

        [Parameter] public EventCallback OnNav { get; set; }

        [Parameter] public RenderFragment ActionItems { get; set; }

        [Inject] protected IJSRuntime JSRuntime { get; set; }

        protected ElementReference mdcTopAppBarElement;

        public string ElementId => mdcTopAppBarElement.Id;

        protected override string BuildClassString()
        {
            var sb = new StringBuilder("mdc-top-app-bar");

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
                await JSRuntime.InvokeVoidAsync("MDCTopAppBarComponent.attachTo", mdcTopAppBarElement);
                await JSRuntime.InvokeVoidAsync("MDCTopAppBarComponent.listenToNav", mdcTopAppBarElement, DotNetObjectReference.Create(this));
            }
        }

        [JSInvokable(nameof(OnMDCTopAppBarNav))]
        public Task OnMDCTopAppBarNav() => OnNav.InvokeAsync(this);
    }
}
