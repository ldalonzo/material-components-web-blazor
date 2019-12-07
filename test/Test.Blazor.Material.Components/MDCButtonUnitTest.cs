using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Button;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using Shouldly;
using Xunit;

namespace Test.Blazor.Material.Components
{
    public class MDCButtonUnitTest : MaterialComponentUnitTest<MDCButton>
    {
        public MDCButtonUnitTest()
            : base(new ServiceCollection().AddSingleton(new Mock<IJSRuntime>().Object))
        {
        }

        [Fact]
        public void TestMandatoryCssClass()
        {
            var component = AddComponent();
            component.GetCssClassForElement("button").ShouldContain("mdc-button");
        }

        [Theory]
        [AutoData]
        public void Label(string label)
        {
            var component = host.AddComponent<MDCButton>(("ChildContent", (RenderFragment)(b => b.AddContent(0, label))));

            var markup = component.GetMarkup();
            markup.ShouldContain(label);
        }

        [Theory]
        [InlineData(MDCButtonStyle.Outlined, "mdc-button--outlined")]
        [InlineData(MDCButtonStyle.Raised, "mdc-button--raised")]
        [InlineData(MDCButtonStyle.Unelevated, "mdc-button--unelevated")]
        public void Css_Variants_ButtonStyle(MDCButtonStyle style, string expectedCssClass)
        {
            var component = host.AddComponent<MDCButton>(("Variant", style));

            var markup = component.GetMarkup();
            markup.ShouldContain("mdc-button");
            markup.ShouldContain(expectedCssClass);
        }

        [Fact]
        public void Css_Variants_Icons_Leading_NotPresent()
        {
            var component = host.AddComponent<MDCButton>(("LeadingIcon", string.Empty));

            var markup = component.GetMarkup();
            markup.ShouldNotContain("material-icons mdc-button__icon");
        }

        [Theory]
        [AutoData]
        public void Css_Variants_Icons_Leading(string iconName)
        {
            var component = host.AddComponent<MDCButton>(("LeadingIcon", iconName));

            var markup = component.GetMarkup();
            markup.ShouldContain("material-icons mdc-button__icon");
            markup.ShouldContain(iconName);
        }

        [Fact]
        public void Click()
        {
            var counter = new Counter();
            var component = host.AddComponent<MDCButton>(("OnClick", EventCallback.Factory.Create<MouseEventArgs>(this, counter.Increment)));

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
