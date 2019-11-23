using AutoFixture.Xunit2;
using Blazor.Material.Components.Button;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.JSInterop;
using Moq;
using Shouldly;
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

        [Theory]
        [AutoData]
        public void TestButtonLabel(string label)
        {
            host.AddService(new Mock<IJSRuntime>().Object);
            var component = host.AddComponent<MDCButton>(("ChildContent", (RenderFragment)(b => b.AddContent(0, label))));

            var markup = component.GetMarkup();
            markup.ShouldContain(label);
        }

        [Fact]
        public void TestCssClasses()
        {
            host.AddService(new Mock<IJSRuntime>().Object);
            var component = host.AddComponent<MDCButton>();

            var markup = component.GetMarkup();
            markup.ShouldContain("mdc-button");
        }

        [Fact]
        public void TestCssClasses_VariantOutlined()
        {
            host.AddService(new Mock<IJSRuntime>().Object);
            var component = host.AddComponent<MDCButton>((nameof(MDCButton.Variant), MDCButtonStyle.Outlined));

            var markup = component.GetMarkup();
            markup.ShouldContain("mdc-button");
            markup.ShouldContain("mdc-button--outlined");
        }
    }
}
