using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Leonardo.AspNetCore.Components.Material.Ripple
{
    public class MDCRipple
    {
        public static ValueTask AttachTo(IJSRuntime runtime, ElementReference element)
            => runtime.InvokeVoidAsync("MDCRippleComponent.attachTo", element);
    }
}
