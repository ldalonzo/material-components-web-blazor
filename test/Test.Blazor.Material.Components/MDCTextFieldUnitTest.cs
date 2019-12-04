using Leonardo.AspNetCore.Components.Material.Drawer;
using Leonardo.AspNetCore.Components.Material.TextField;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.JSInterop;
using Moq;
using Shouldly;
using Xunit;

namespace Test.Blazor.Material.Components
{
    public class MDCTextFieldUnitTest
    {
        public MDCTextFieldUnitTest()
        {
            host = new TestHost();
            host.AddService(new Mock<IJSRuntime>().Object);
        }

        private readonly TestHost host;

        [Fact]
        public void TestCreation()
        {
            var component = host.AddComponent<MDCTextField>();
            component.Instance.ShouldNotBeNull();
        }
    }
}
