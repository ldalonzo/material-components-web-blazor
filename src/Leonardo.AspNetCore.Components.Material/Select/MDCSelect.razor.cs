using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Leonardo.AspNetCore.Components.Material.Select
{
    /// <summary>
    /// MDC Select provides Material Design single-option select menus, using the MDC menu.
    /// The Select component is fully accessible, and supports RTL rendering.
    /// </summary>
    /// <seealso href="https://github.com/material-components/material-components-web/tree/master/packages/mdc-select"/>
    public abstract class MDCSelect : MaterialComponent
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

    public partial class MDCSelect<T> : MDCSelect
    {
        [Parameter] public IList<T> DataSource { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (DataSource == null)
            {
                DataSource = new List<T>();
            }
        }

        private string GetDataValue(T item)
        {
            return item.ToString();
        }

        private string GetDisplayText(T item)
        {
            return item.ToString();
        }
    }
}
