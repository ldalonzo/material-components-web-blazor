using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Foundations;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components
{
    internal class MDCDrawer : MDCComponent<MDCDismissibleDrawerFoundation>
    {
        public bool Open { get; set; }
    }
}
