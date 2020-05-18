using Leonardo.AspNetCore.Components.Material.TabBar;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Foundations;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components
{
    internal class MDCTabBarFake : MDCComponentFake<MDCTabBarFoundation>
    {
        public void SetActiveTab(int index)
        {
            Emit("MDCTabBar:activated", new MDCTabBarActivatedEventDetail { Index = index });
        }
    }
}
