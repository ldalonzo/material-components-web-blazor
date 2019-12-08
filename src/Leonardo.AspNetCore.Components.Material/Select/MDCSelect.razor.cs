using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;
using System.Threading.Tasks;

namespace Leonardo.AspNetCore.Components.Material.Select
{
    /// <summary>
    /// MDC Select provides Material Design single-option select menus, using the MDC menu.
    /// The Select component is fully accessible, and supports RTL rendering.
    /// </summary>
    /// <seealso href="https://github.com/material-components/material-components-web/tree/master/packages/mdc-select"/>
    public partial class MDCSelect : MaterialComponent
    {
        [Parameter] public string Label { get; set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }

        protected ElementReference mdcSelectElement;

        protected override string BuildClassString()
        {
            var sb = new StringBuilder();

            sb.Append("mdc-select");

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
                await JSRuntime.InvokeVoidAsync("MDCSelectComponent.attachTo", mdcSelectElement);
            }
        }
    }
}
