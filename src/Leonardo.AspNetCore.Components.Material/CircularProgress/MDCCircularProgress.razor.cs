using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Text;
using System.Threading.Tasks;

namespace Leonardo.AspNetCore.Components.Material.CircularProgress
{
    public partial class MDCCircularProgress
    {
        [Parameter] public bool Indeterminate { get; set; }

        [Parameter] public string Label { get; set; }

        [Inject] private IJSRuntime JSRuntime { get; set; }

        protected ElementReference _MDCCircularProgress;

        protected override StringBuilder BuildClassString(StringBuilder sb)
        {
            sb.Append("mdc-circular-progress");

            sb.Append(" ");
            sb.Append("mdc-circular-progress--large");

            return base.BuildClassString(sb);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("MDCCircularProgressComponent.attachTo", _MDCCircularProgress, Id);
            }

            await SetDeterminate(!Indeterminate);
        }

        /// <summary>
        /// Toggles the component between the determinate and indeterminate state.
        /// </summary>
        public async Task SetDeterminate(bool value)
            => await JSRuntime.InvokeVoidAsync("MDCCircularProgressComponent.setDeterminate", Id, value);
    }
}
