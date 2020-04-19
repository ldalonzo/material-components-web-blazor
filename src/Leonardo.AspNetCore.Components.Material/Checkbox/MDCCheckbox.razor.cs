using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;
using System.Threading.Tasks;

namespace Leonardo.AspNetCore.Components.Material.Checkbox
{
    /// <summary>
    /// Checkboxes allow the user to select one or more items from a set.
    /// </summary>
    /// <seealso href="https://github.com/material-components/material-components-web/tree/master/packages/mdc-checkbox"/>
    public partial class MDCCheckbox
    {
        [Parameter] public string Label { get; set; }

        [Parameter] public bool Value { get; set; }

        [Parameter] public EventCallback<bool> ValueChanged { get; set; }

        [Parameter] public bool Disabled { get; set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }

        protected ElementReference mdcCheckboxElement;

        private Task OnValueChanged(ChangeEventArgs e)
        {
            var value = (bool)e.Value;
            if (value != Value)
            {
                Value = value;
                return ValueChanged.InvokeAsync(Value);
            }

            return Task.CompletedTask;
        }

        private string LabelId => $"{Id}-label";

        protected override StringBuilder BuildClassString(StringBuilder sb)
        {
            sb.Append("mdc-form-field");

            return base.BuildClassString(sb);
        }

        private string CheckboxCssClass { get; set; }

        private string BuildCheckboxCssClass()
        {
            var sb = new StringBuilder();

            sb.Append("mdc-checkbox");

            if (Disabled)
            {
                sb.Append($" mdc-checkbox--disabled");
            }

            return sb.ToString();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            CheckboxCssClass = BuildCheckboxCssClass();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await Attach();
            }

            await SetChecked();
        }

        private async Task Attach()
            => await JSRuntime.InvokeVoidAsync("MDCCheckboxComponent.attachTo", mdcCheckboxElement);

        private async Task SetChecked()
            => await JSRuntime.InvokeVoidAsync("MDCCheckboxComponent.setChecked", mdcCheckboxElement, Value);
    }
}
