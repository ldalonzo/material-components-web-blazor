using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Button;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.AspNetCore.Components.Web;
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

        [Theory]
        [InlineData(MDCButtonStyle.Outlined, "mdc-button--outlined")]
        [InlineData(MDCButtonStyle.Raised, "mdc-button--raised")]
        [InlineData(MDCButtonStyle.Unelevated, "mdc-button--unelevated")]
        public void Css_Variants_ButtonStyle(MDCButtonStyle style, string expectedCssClass)
        {
            var component = host.AddComponent<MDCButton>((nameof(MDCButton.Variant), style));

            var markup = component.GetMarkup();
            markup.ShouldContain("mdc-button");
            markup.ShouldContain(expectedCssClass);
        }

        [Fact]
        public void Css_Variants_Icons_Leading_NotPresent()
        {
            var component = host.AddComponent<MDCButton>((nameof(MDCButton.LeadingIcon), string.Empty));

            var markup = component.GetMarkup();
            markup.ShouldNotContain("material-icons mdc-button__icon");
        }

        [Theory]
        [AutoData]
        public void Css_Variants_Icons_Leading(string iconName)
        {
            var component = host.AddComponent<MDCButton>((nameof(MDCButton.LeadingIcon), iconName));

            var markup = component.GetMarkup();
            markup.ShouldContain("material-icons mdc-button__icon");
            markup.ShouldContain(iconName);
        }

        [Fact]
        public void Click()
        {
            var counter = new Counter();
            var component = host.AddComponent<MDCButton>((nameof(MDCButton.OnClick), EventCallback.Factory.Create<MouseEventArgs>(this, counter.Increment)));

            component.Find("button").Click();

            counter.Count.ShouldBe(1);
        }

        private class Counter
        {
            public int Count { get; private set; }

            public void Increment() => Count++;
        }
    }
}
