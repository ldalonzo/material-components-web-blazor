using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;
using System.Threading.Tasks;

namespace Leonardo.AspNetCore.Components.Material.TextField
{
    /// <summary>
    /// Text fields allow users to enter text into a UI. They typically appear in forms and dialogs.
    /// </summary>
    /// <seealso href="https://material.io/develop/web/components/input-controls/text-field/"/>
    public partial class MDCTextField
    {
        [Parameter] public string Label { get; set; }

        [Parameter] public bool Disabled { get; set; }

        [Parameter] public string Value { get; set; }

        [Parameter] public EventCallback<string> ValueChanged { get; set; }

        private Task OnValueChanged(ChangeEventArgs e)
        {
            Value = e.Value.ToString();
            return ValueChanged.InvokeAsync(Value);
        }

        [Parameter] public MDCTextFieldStyle Variant { get; set; } = MDCTextFieldStyle.Filled;

        [Inject] protected IJSRuntime JSRuntime { get; set; }

        protected ElementReference mdcTextFieldElement;

        private string LabelId { get; set; }

        protected override StringBuilder BuildClassString(StringBuilder sb)
        {
            sb.Append("mdc-text-field");


            switch (Variant)
            {
                case MDCTextFieldStyle.Filled:
                    sb.Append(" mdc-text-field--filled");
                    break;
                case MDCTextFieldStyle.Outlined:
                    sb.Append(" mdc-text-field--outlined");
                    break;
            }

            if (Disabled)
            {
                sb.Append(" mdc-text-field--disabled");
            }

            return base.BuildClassString(sb);
        }

        protected string LabelClassString { get; private set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            LabelClassString = BuildLabelClassString();
            NotchedOutlineClassString = BuildNotchedOutlineClassString();

            if (string.IsNullOrWhiteSpace(LabelId))
            {
                LabelId = $"{Id}-label";
            }
        }

        private string BuildLabelClassString()
        {
            var sb = new StringBuilder();

            sb.Append("mdc-floating-label");

            if (!string.IsNullOrWhiteSpace(Value))
            {
                // Ensure that the label moves out of the way of the text field's value and prevents a Flash Of Un-styled Content (FOUC).
                sb.Append(" mdc-floating-label--float-above");
            }

            return sb.ToString();
        }

        protected string NotchedOutlineClassString { get; private set; }

        private string BuildNotchedOutlineClassString()
        {
            var sb = new StringBuilder();

            sb.Append("mdc-notched-outline");

            if (!string.IsNullOrWhiteSpace(Value))
            {
                sb.Append(" mdc-notched-outline--notched");
            }

            return sb.ToString();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("MDCTextFieldComponent.attachTo", mdcTextFieldElement, Id);
            }
        }
    }
}
