using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop
{
    internal class MDCSwitchJsInteropFake : MDCComponentJsInterop<MDCSwitchFake>
    {
        protected override string ComponentIdentifier => "MDCSwitchComponent";
    }
}
