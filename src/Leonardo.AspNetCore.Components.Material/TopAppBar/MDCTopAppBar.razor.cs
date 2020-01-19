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

        [Inject] protected IJSRuntime JSRuntime { get; set; }

        protected ElementReference mdcTopAppBarElement;

        private string Id { get; set; }

        protected override string BuildClassString()
        {
            var sb = new StringBuilder("mdc-top-app-bar");

            if (!string.IsNullOrWhiteSpace(Class))
            {
                sb.Append($" {Class}");
            }

            return sb.ToString();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (string.IsNullOrWhiteSpace(Id))
            {
                Id = $"{GetType().Name}-{Guid.NewGuid().ToString().Substring(0, 4)}".ToLower();
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("MDCTopAppBarComponent.attachTo", mdcTopAppBarElement);
            }
        }
    }
}
