using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Foundations;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components
{
    internal class MDCListFake : MDCComponentFake<MDCListFoundation>
    {
        public bool WrapFocus { get; set; }
    }
}
