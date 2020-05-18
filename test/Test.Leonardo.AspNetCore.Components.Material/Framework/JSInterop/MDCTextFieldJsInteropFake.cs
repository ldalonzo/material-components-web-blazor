using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop
{
    internal class MDCTextFieldJsInteropFake : MDCComponentJsInterop<MDCTextFieldFake>
    {
        protected override string ComponentIdentifier => "MDCTextFieldComponent";
    }
}
