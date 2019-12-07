using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Leonardo.AspNetCore.Components.Material.TextField
{
    /// <summary>
    /// Text fields allow users to input, edit, and select text.
    /// </summary>
    /// <seealso href="https://material.io/develop/web/components/input-controls/text-field/"/>
    public partial class MDCTextField : MaterialComponent
    {
        [Parameter] public string Label { get; set; }

        [Parameter] public string Value { get; set; }

        [Parameter] public EventCallback<string> ValueChanged { get; set; }

        private Task OnValueChanged(ChangeEventArgs e)
        {
            Value = e.Value.ToString();
            return ValueChanged.InvokeAsync(Value);
        }

        [Inject] protected IJSRuntime JSRuntime { get; set; }

        protected ElementReference mdcTextFieldElement;

        protected string Id { get; set; } = $"text-field-{Guid.NewGuid().ToString().Substring(0, 3).ToLower()}";

        protected override string BuildClassString()
        {
            var sb = new StringBuilder();

            sb.Append("mdc-text-field");

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
                await JSRuntime.InvokeVoidAsync("MDCTextFieldComponent.attachTo", mdcTextFieldElement);
            }
        }
    }
}
