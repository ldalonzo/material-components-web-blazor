using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Text;
using System.Threading.Tasks;

namespace Leonardo.AspNetCore.Components.Material.Button
{
    /// <summary>
    /// Buttons allow users to take actions, and make choices, with a single tap.
    /// </summary>
    /// <see href="https://material.io/develop/web/components/buttons/"/>
    public partial class MDCButton : MaterialComponent
    {
        private const string MDCRippleComponent_AttachTo = "MDCRippleComponent.attachTo";

        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter] public string LeadingIcon { get; set; }

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter] public MDCButtonStyle Variant { get; set; } = MDCButtonStyle.Text;

        [Inject] protected IJSRuntime JSRuntime { get; set; }

        protected ElementReference _MDCButton;

        protected override string BuildClassString()
        {
            var sb = new StringBuilder();

            sb.Append("mdc-button");

            switch (Variant)
            {
                case MDCButtonStyle.Outlined:
                    sb.Append(" mdc-button--outlined");
                    break;

                case MDCButtonStyle.Raised:
                    sb.Append(" mdc-button--raised");
                    break;

                case MDCButtonStyle.Unelevated:
                    sb.Append(" mdc-button--unelevated");
                    break;
            }

            return sb.ToString();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync(MDCRippleComponent_AttachTo, _MDCButton);
            }
        }
    }
}
