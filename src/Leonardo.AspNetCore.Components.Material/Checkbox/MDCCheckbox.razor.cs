using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Leonardo.AspNetCore.Components.Material.Checkbox
{
    /// <summary>
    /// Checkboxes allow the user to select one or more items from a set.
    /// </summary>
    /// <seealso href="https://github.com/material-components/material-components-web/tree/master/packages/mdc-checkbox"/>
    public partial class MDCCheckbox
    {
        [Parameter] public bool Disabled { get; set; }

        [Parameter] public bool Indeterminate { get; set; }

        [Parameter] public EventCallback<bool> IndeterminateChanged { get; set; }

        private Task OnIndeterminateChanged()
        {
            if (Indeterminate)
            {
                Indeterminate = false;
            }

            return IndeterminateChanged.InvokeAsync(Indeterminate);
        }

        [Parameter] public string Label { get; set; }

        [Parameter] public bool Value { get; set; }

        [Parameter] public EventCallback<bool> ValueChanged { get; set; }

        private async Task OnValueChanged(ChangeEventArgs e)
        {
            var value = (bool)e.Value;

            Value = value;

            await OnIndeterminateChanged();
            await ValueChanged.InvokeAsync(Value);
        }

        [Inject] private IJSRuntime JSRuntime { get; set; }

        protected ElementReference _MDCCheckbox;

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
            await SetIndeterminate();
        }

        private async Task Attach()
            => await JSRuntime.InvokeVoidAsync("MDCCheckboxComponent.attachTo", _MDCCheckbox, Id);

        private async Task SetChecked()
            => await JSRuntime.InvokeVoidAsync("MDCCheckboxComponent.setChecked", Id, Value);

        private async Task SetIndeterminate()
            => await JSRuntime.InvokeVoidAsync("MDCCheckboxComponent.setIndeterminate", Id, Indeterminate);
    }
}
