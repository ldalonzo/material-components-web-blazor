using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.TabBar;
using Shouldly;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCTabUnitTest : MaterialComponentUnitTest<MDCTab>
    {
        [Fact]
        public void HtmlStructure_MdcTab()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/button").ShouldHaveSingleItem();
            divElement.ShouldContainCssClasses("mdc-tab");
        }

        [Fact]
        public void HtmlStructure_MdcTab_Role()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/button").ShouldHaveSingleItem();

            var roleAttribute = divElement.Attributes["role"];
            roleAttribute.ShouldNotBeNull();
            roleAttribute.Value.ShouldBe("tab");
        }

        [Fact]
        public void HtmlStructure_MdcTabContent()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var tabContent = rootNode.SelectNodes("/button/span[1]").ShouldHaveSingleItem();

            var tabContentClass = tabContent.Attributes["class"];
            tabContentClass.ShouldNotBeNull();
            tabContentClass.Value.ShouldBe("mdc-tab__content");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcTabContent_Label(string label)
        {
            var sut = AddComponent(("Label", label));

            var rootNode = sut.GetDocumentNode();
            var textLabel = rootNode.SelectNodes("/button/span[1]/span").ShouldHaveSingleItem();

            textLabel.ShouldContainCssClasses("mdc-tab__text-label");
            textLabel.InnerText.Trim().ShouldBe(label);
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcTabContent_LabelWithIcon(string label, string icon)
        {
            var sut = AddComponent(
                ("Label", label),
                ("Icon", icon));

            var rootNode = sut.GetDocumentNode();
            var textLabel = rootNode.SelectNodes("/button/span[1]/span[2]").ShouldHaveSingleItem();
            textLabel.ShouldContainCssClasses("mdc-tab__text-label");

            var iconLabel = rootNode.SelectNodes("/button/span[1]/span[1]").ShouldHaveSingleItem();
            iconLabel.ShouldContainCssClasses("mdc-tab__icon", "material-icons");
            iconLabel.InnerText.Trim().ShouldBe(icon);

            var ariaAttribute = iconLabel.Attributes["aria-hidden"];
            ariaAttribute.ShouldNotBeNull();
            ariaAttribute.Value.ShouldBe("true");
        }

        [Fact]
        public void HtmlStructure_MdcTabIndicator()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var tabIndicator = rootNode.SelectNodes("/button/span[2]").ShouldHaveSingleItem();

            var tabIndicatorClass = tabIndicator.Attributes["class"];
            tabIndicatorClass.ShouldNotBeNull();
            tabIndicatorClass.Value.ShouldBe("mdc-tab-indicator");
        }

        [Fact]
        public void HtmlStructure_MdcTabIndicator_Content()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var tabIndicatorContent = rootNode.SelectNodes("/button/span[2]/span").ShouldHaveSingleItem();

            tabIndicatorContent.ShouldContainCssClasses("mdc-tab-indicator__content", "mdc-tab-indicator__content--underline");
        }

        [Fact]
        public void HtmlStructure_MdcTabRipple()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var ripple = rootNode.SelectNodes("/button/span[3]").ShouldHaveSingleItem();

            var rippleClass = ripple.Attributes["class"];
            rippleClass.ShouldNotBeNull();
            rippleClass.Value.ShouldBe("mdc-tab__ripple");
        }
    }
}
