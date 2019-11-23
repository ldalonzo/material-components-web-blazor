using Blazor.Material.Components.Drawer;
using LD.AspNetCore.Components.Testing;
using Shouldly;
using System.Linq;
using Xunit;

namespace Test.Blazor.Material.Components
{
    public class MDCDrawerUnitTest
    {
        TestHost host = new TestHost();

        [Fact]
        public void TestCreation()
        {
            var component = host.AddComponent<MDCDrawer>();
            component.Item.ShouldNotBeNull();
        }

        [Fact]
        public void TestCssClasses()
        {
            var component = host.AddComponent<MDCDrawer>();

            component.Item.ClassString.ShouldNotBeNull();
            component.Item.ClassString.Split().Where(r => r == "mdc-drawer").ShouldHaveSingleItem();
        }
    }
}
