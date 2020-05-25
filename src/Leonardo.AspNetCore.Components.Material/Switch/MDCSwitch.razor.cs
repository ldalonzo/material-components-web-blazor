using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Leonardo.AspNetCore.Components.Material.Switch
{
    /// <summary>
    /// Switches toggle the state of a single setting on or off. They are the preferred way to adjust settings on mobile.
    /// </summary>
    public partial class MDCSwitch
    {
        [Parameter] public string Label { get; set; }

        [Inject] private IJSRuntime JSRuntime { get; set; }

        protected ElementReference _MDCSwitch;

        private string InputId { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (string.IsNullOrWhiteSpace(InputId))
            {
                InputId = $"{Id}-input";
            }
        }

        protected override StringBuilder BuildClassString(StringBuilder sb)
        {
            sb.Append("mdc-switch");

            return base.BuildClassString(sb);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("MDCSwitchComponent.attachTo", _MDCSwitch, Id);
            }
        }
    }
}
