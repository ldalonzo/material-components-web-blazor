using Blazor.Material.Components.Button;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.JSInterop;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Test.Blazor.Material.Components
{
    public class MDCButtonUnitTest
    {
        private readonly TestHost host = new TestHost();

        [Fact]
        public void TestCreation()
        {
            host.AddService(new Mock<IJSRuntime>().Object);
            var component = host.AddComponent<MDCButton>();
            component.Instance.ShouldNotBeNull();
        }

        [Fact]
        public void TestCssClasses()
        {
            host.AddService(new Mock<IJSRuntime>().Object);
            var component = host.AddComponent<MDCButton>();

            component.Instance.ClassString.ShouldNotBeNull();
            component.Instance.ClassString.Split().Where(r => r == "mdc-button").ShouldHaveSingleItem();
        }

        [Fact]
        public void TestCssClasses_VariantOutlined()
        {
            var parameters = new Dictionary<string, object>() {
                {nameof(MDCButton.Variant), MDCButtonStyle.Outlined }
            };

            host.AddService(new Mock<IJSRuntime>().Object);
            var component = host.AddComponent<MDCButton>(parameters.Select(r=>r).ToArray());

            component.Instance.ClassString.ShouldNotBeNull();
            component.Instance.ClassString.Split().Where(r => r == "mdc-button").ShouldHaveSingleItem();
            component.Instance.ClassString.Split().Where(r => r == "mdc-button--outlined").ShouldHaveSingleItem();
        }
    }
}
