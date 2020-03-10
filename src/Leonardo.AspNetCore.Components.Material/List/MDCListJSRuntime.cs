using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Leonardo.AspNetCore.Components.Material.List
{
    public class MDCListJSRuntime
    {
        public static ValueTask AttachTo(IJSRuntime runtime, ElementReference element, bool wrapFocus)
            => runtime.InvokeVoidAsync("MDCListComponent.attachTo", element, wrapFocus);
    }
}
