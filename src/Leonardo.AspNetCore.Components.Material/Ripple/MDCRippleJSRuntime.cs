using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Leonardo.AspNetCore.Components.Material.Ripple
{
    public class MDCRippleJSRuntime
    {
        public static ValueTask AttachTo(IJSRuntime runtime, ElementReference element)
            => runtime.InvokeVoidAsync("MDCRippleComponent.attachTo", element);
    }
}
