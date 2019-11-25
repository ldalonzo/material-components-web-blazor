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
        public MDCButtonUnitTest()
        {
            host = new TestHost();
            host.AddService(new Mock<IJSRuntime>().Object);
        }

        private readonly TestHost host;

        [Fact]
        public void Lifecyle_Creation()
        {
            var component = host.AddComponent<MDCButton>();
            component.Instance.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Label(string label)
        {
            var component = host.AddComponent<MDCButton>(("ChildContent", (RenderFragment)(b => b.AddContent(0, label))));

            var markup = component.GetMarkup();
            markup.ShouldContain(label);
        }

        [Fact]
        public void Css()
        {
            var component = host.AddComponent<MDCButton>();

            var markup = component.GetMarkup();
            markup.ShouldContain("mdc-button");
        }

        [Fact]
        public void Css_Variants_Outlined()
        {
            var component = host.AddComponent<MDCButton>((nameof(MDCButton.Variant), MDCButtonStyle.Outlined));

            var markup = component.GetMarkup();
            markup.ShouldContain("mdc-button");
            markup.ShouldContain("mdc-button--outlined");
        }

        [Fact]
        public void Css_Variants_Icons_Leading_NotPresent()
        {
            var component = host.AddComponent<MDCButton>((nameof(MDCButton.LeadingMaterialIconName), string.Empty));

            var markup = component.GetMarkup();
            markup.ShouldNotContain("material-icons mdc-button__icon");
        }

        [Theory]
        [AutoData]
        public void Css_Variants_Icons_Leading(string iconName)
        {
            var component = host.AddComponent<MDCButton>((nameof(MDCButton.LeadingMaterialIconName), iconName));

            var markup = component.GetMarkup();
            markup.ShouldContain("material-icons mdc-button__icon");
            markup.ShouldContain(iconName);
        }
    }
}
