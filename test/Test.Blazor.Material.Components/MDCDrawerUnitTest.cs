using Blazor.Material.Components.Drawer;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.JSInterop;
using Moq;
using Shouldly;
using System.Linq;
using Xunit;

namespace Test.Blazor.Material.Components
{
    public class MDCDrawerUnitTest
    {
        private readonly TestHost host = new TestHost();

        [Fact]
        public void TestCreation()
        {
            host.AddService(new Mock<IJSRuntime>().Object);
            var component = host.AddComponent<MDCDrawer>();
            component.Instance.ShouldNotBeNull();
        }

        [Fact]
        public void TestCssClasses()
        {
            host.AddService(new Mock<IJSRuntime>().Object);
            var component = host.AddComponent<MDCDrawer>();

            component.Instance.ClassString.ShouldNotBeNull();
            component.Instance.ClassString.Split().Where(r => r == "mdc-drawer").ShouldHaveSingleItem();
        }
    }
}
