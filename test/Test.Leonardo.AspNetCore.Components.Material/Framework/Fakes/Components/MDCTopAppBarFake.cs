using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Foundations;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components
{
    internal class MDCTopAppBarFake : MDCComponentFake<MDCTopAppBarBaseFoundation>
    {
        public Task HandleNavigationClick()
        {
            return Emit("MDCTopAppBar:nav");
        }
    }
}
