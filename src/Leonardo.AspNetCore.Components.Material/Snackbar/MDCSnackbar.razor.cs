using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;
using System.Threading.Tasks;

namespace Leonardo.AspNetCore.Components.Material.Snackbar
{
    /// <summary>
    /// Snackbars provide brief messages about app processes at the bottom of the screen.
    /// </summary>
    public partial class MDCSnackbar
    {
        [Parameter] public string Text { get; set; }

        [Parameter] public string ButtonLabel { get; set; }

        [Inject] protected IJSRuntime JSRuntime { get; set; }

        protected ElementReference _MDCSnackbar;

        public string ElementReferenceId => _MDCSnackbar.Id;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (Text == null)
            {
                Text = string.Empty;
            }
        }

        protected override StringBuilder BuildClassString(StringBuilder sb)
        {
            sb.Append("mdc-snackbar");

            return base.BuildClassString(sb);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("MDCSnackbarComponent.attachTo", _MDCSnackbar);
            }

            await SetLabelText();
        }

        /// <summary>
        /// Opens the snackbar.
        /// </summary>
        public async Task Open()
            => await JSRuntime.InvokeVoidAsync("MDCSnackbarComponent.open", _MDCSnackbar);

        /// <summary>
        /// Sets the textContent of the label element.
        /// </summary>
        public async Task SetLabelText()
            => await JSRuntime.InvokeVoidAsync("MDCSnackbarComponent.setLabelText", _MDCSnackbar, Text);
    }
}
