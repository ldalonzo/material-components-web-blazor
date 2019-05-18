using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.Material
{
    public class BlazorMaterialComponent : ComponentBase
    {
        [Parameter] protected string Class { get; set; }

        [Inject] protected IJSRuntime JSRuntime { get; set; }
    }
}
