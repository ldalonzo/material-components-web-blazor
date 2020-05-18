using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;
using System.Threading.Tasks;

namespace Leonardo.AspNetCore.Components.Material.TopAppBar
{
    /// <summary>
    /// MDC Top App Bar acts as a container for items such as application title, navigation icon, and action items.
    /// </summary>
    /// <seealso href="https://material.io/develop/web/components/top-app-bar/"/>
    public partial class MDCTopAppBar
    {
        [Parameter] public string Title { get; set; }

        [Parameter] public EventCallback OnNav { get; set; }

        [Parameter] public RenderFragment ActionItems { get; set; }

        [Inject] protected IJSRuntime JSRuntime { get; set; }

        protected ElementReference mdcTopAppBarElement;

        protected override StringBuilder BuildClassString(StringBuilder sb)
        {
            sb.Append("mdc-top-app-bar");

            return base.BuildClassString(sb);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("MDCTopAppBarComponent.attachTo", mdcTopAppBarElement, Id);
                await JSRuntime.InvokeVoidAsync("MDCTopAppBarComponent.listenToNav", Id, DotNetObjectReference.Create(this));
            }
        }

        [JSInvokable(nameof(OnMDCTopAppBarNav))]
        public Task OnMDCTopAppBarNav() => OnNav.InvokeAsync(this);
    }
}
