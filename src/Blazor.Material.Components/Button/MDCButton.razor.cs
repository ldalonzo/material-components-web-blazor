using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
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

        [Parameter] public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter] public RenderFragment ChildContent { get; set; }

        [Inject] protected IJSRuntime JSRuntime { get; set; }

        public string ClassString { get; private set; }

        protected ElementReference _MDCButton;

        protected override void OnInitialized()
        {
            base.OnInitialized();

            ClassString = "mdc-button";
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeAsync<bool>(MDCRippleComponent_AttachTo, _MDCButton);
            }
        }
    }
}
