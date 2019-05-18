using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Blazor.Material.Button
{
    /// <summary>
    /// Buttons allow users to take actions, and make choices, with a single tap.
    /// </summary>
    /// <see href="https://material.io/develop/web/components/buttons/"/>
    public class MDCButtonComponent : BlazorMaterialComponent
    {
        private const string MDCRippleComponentAttachTo = "MDCRippleComponent.attachTo";

        [Parameter]
        protected EventCallback<UIMouseEventArgs> OnClick { get; set; }

        [Parameter]
        protected RenderFragment ChildContent { get; set; }

        protected string ClassString { get; private set; }

        protected ElementRef _MDCButton;
        private bool _isFirstRender = true;

        protected override void OnInit()
        {
            ClassString = "mdc-button";
        }

        protected override async Task OnAfterRenderAsync()
        {
            if (_isFirstRender)
            {
                _isFirstRender = false;
                await JSRuntime.InvokeAsync<bool>(MDCRippleComponentAttachTo, _MDCButton);
            }
        }
    }
}
