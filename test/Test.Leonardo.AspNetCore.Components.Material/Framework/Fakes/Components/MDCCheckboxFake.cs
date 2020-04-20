using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Foundations;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components
{
    internal class MDCCheckboxFake : MDCComponentFake<MDCCheckboxFoundation>
    {
        public bool Checked { get; set; }
    }
}
