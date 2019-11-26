using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Material.Components.Button
{
    /// <summary>
    /// Buttons allow users to take actions, and make choices, with a single tap.
    /// </summary>
    /// <see href="https://material.io/develop/web/components/buttons/"/>
    public class MDCButtonComponent : MaterialComponent
    {
        private const string MDCRippleComponent_AttachTo = "MDCRippleComponent.attachTo";

        [Parameter] public MDCButtonStyle Variant { get; set; } = MDCButtonStyle.Text;

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter] public string LeadingMaterialIconName { get; set; }

        [Parameter] public RenderFragment ChildContent { get; set; }

        [Inject] protected IJSRuntime JSRuntime { get; set; }

        protected string ClassString { get; private set; }

        protected ElementReference _MDCButton;

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            ClassString = BuildClassString();
        }

        private string BuildClassString()
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
