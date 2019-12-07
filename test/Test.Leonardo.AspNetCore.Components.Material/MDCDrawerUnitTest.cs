using Leonardo.AspNetCore.Components.Material.Drawer;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.JSInterop;
using Moq;
using Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
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

            var markup = component.GetMarkup();
            markup.ShouldContain("mdc-drawer");
        }
    }
}
