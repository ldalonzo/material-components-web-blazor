using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Button;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Moq;
using Shouldly;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCButtonUnitTest : MaterialComponentUnitTest<MDCButton>
    {
        public MDCButtonUnitTest()
            : base(h => h.AddService(new Mock<IJSRuntime>().Object))
        {
        }

        [Fact]
        public void TextButton_MandatoryCssClasses()
        {
            var sut = AddComponent(("Variant", MDCButtonStyle.Text));

            var rootNode = sut.GetDocumentNode();
            var buttonElement = rootNode.SelectNodes("/button").ShouldHaveSingleItem();
            buttonElement.ShouldContainCssClasses("mdc-button");
        }

        [Fact]
        public void Style_Outlined_HasMandatoryCssClasses()
        {
            var button = AddComponent(("Variant", MDCButtonStyle.Outlined));
            button.Find("button").ShouldContainCssClasses("mdc-button", "mdc-button--outlined");
        }

        [Fact]
        public void Style_Raised_HasMandatoryCssClasses()
        {
            var button = AddComponent(("Variant", MDCButtonStyle.Raised));
            button.Find("button").ShouldContainCssClasses("mdc-button", "mdc-button--raised");
        }

        [Fact]
        public void Style_Unelevated_HasMandatoryCssClasses()
        {
            var button = AddComponent(("Variant", MDCButtonStyle.Unelevated));
            button.Find("button").ShouldContainCssClasses("mdc-button", "mdc-button--unelevated");
        }

        [Theory]
        [AutoData]
        public void Label_IsRendered(string label)
        {
            var component = host.AddComponent<MDCButton>(("ChildContent", (RenderFragment)(b => b.AddContent(0, label))));

            var markup = component.GetMarkup();
            markup.ShouldContain(label);
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

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Disabled(bool disabled)
        {
            var sut = AddComponent(("Disabled", disabled));

            var rootNode = sut.GetDocumentNode();
            var buttonElement = rootNode.SelectNodes("/button").ShouldHaveSingleItem();

            var disabledAttribute = buttonElement.Attributes["disabled"];

            if (disabled)
            {
                disabledAttribute.ShouldNotBeNull();
            }
            else
            {
                disabledAttribute.ShouldBeNull();
            }
        }

        private class Counter
        {
            public int Count { get; private set; }

            public void Increment() => Count++;
        }
    }
}
