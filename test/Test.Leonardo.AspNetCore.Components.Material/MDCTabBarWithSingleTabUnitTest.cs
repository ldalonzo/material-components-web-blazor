using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Testing.TabBar;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.JSInterop;
using Shouldly;
using Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCTabBarWithSingleTabUnitTest
    {
        public MDCTabBarWithSingleTabUnitTest()
        {
            host = new TestHost();

            var mdTabBarJsInterop = new MDCTabBarJsInteropFake();
            host.AddService<IJSRuntime, JSRuntimeFake>(new JSRuntimeFake(mdTabBarJsInterop));
        }

        private readonly TestHost host;

        [Fact]
        public void HtmlStructure_TabComponents()
        {
            var sut = host.AddComponent<TabBarWithSingleTab>();

            var rootNode = sut.GetDocumentNode();
            var tabElement = rootNode.SelectNodes("/div/div/div/div/button").ShouldHaveSingleItem();
            tabElement.ShouldContainCssClasses("mdc-tab", "mdc-tab--active");
        }

        [Fact]
        public void HtmlStructure_TabComponents_Role()
        {
            var sut = host.AddComponent<TabBarWithSingleTab>();

            var rootNode = sut.GetDocumentNode();
            var tabElement = rootNode.SelectNodes("/div/div/div/div/button").ShouldHaveSingleItem();
            var roleAttribute = tabElement.Attributes["role"];
            roleAttribute.ShouldNotBeNull();
            roleAttribute.Value.ShouldBe("tab");
        }

        [Fact]
        public void HtmlStructure_TabComponents_AriaSelected()
        {
            var sut = host.AddComponent<TabBarWithSingleTab>();

            var rootNode = sut.GetDocumentNode();
            var tabElement = rootNode.SelectNodes("/div/div/div/div/button").ShouldHaveSingleItem();
            var ariaSelected = tabElement.Attributes["aria-selected"];
            ariaSelected.ShouldNotBeNull();
        }

        [Fact]
        public void HtmlStructure_TabComponents_TabIndex()
        {
            var sut = host.AddComponent<TabBarWithSingleTab>();

            var rootNode = sut.GetDocumentNode();
            var tabElement = rootNode.SelectNodes("/div/div/div/div/button").ShouldHaveSingleItem();
            var tabIndex = tabElement.Attributes["tabindex"];
            tabIndex.ShouldNotBeNull();
            tabIndex.Value.ShouldBe("0");
        }

        [Fact]
        public void HtmlStructure_TabComponents_TabContent()
        {
            var sut = host.AddComponent<TabBarWithSingleTab>();

            var rootNode = sut.GetDocumentNode();
            var tabContent = rootNode.SelectNodes("/div/div/div/div/button/span[1]").ShouldHaveSingleItem();
            tabContent.ShouldContainCssClasses("mdc-tab__content");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_TabComponents_TabContent_Label(string label)
        {
            var sut = host.AddComponent<TabBarWithSingleTab>(("TabLabel", label));

            var rootNode = sut.GetDocumentNode();
            var tabContentLabel = rootNode.SelectNodes("/div/div/div/div/button/span[1]/span").ShouldHaveSingleItem();
            tabContentLabel.ShouldContainCssClasses("mdc-tab__text-label");
            tabContentLabel.InnerText.ShouldBe(label);
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_TabComponents_TabContent_Icon(string label, string icon)
        {
            var sut = host.AddComponent<TabBarWithSingleTab>(
                ("TabLabel", label),
                ("TabIcon", icon));

            var rootNode = sut.GetDocumentNode();
            var tabContentIcon = rootNode.SelectNodes("/div/div/div/div/button/span[1]/span[1]").ShouldHaveSingleItem();
            tabContentIcon.ShouldContainCssClasses("mdc-tab__icon", "material-icons");
            tabContentIcon.InnerText.ShouldBe(icon);
        }

        [Fact]
        public void HtmlStructure_TabComponents_TabIndicator()
        {
            var sut = host.AddComponent<TabBarWithSingleTab>();

            var rootNode = sut.GetDocumentNode();
            var tabIndicator = rootNode.SelectNodes("/div/div/div/div/button/span[2]").ShouldHaveSingleItem();
            tabIndicator.ShouldContainCssClasses("mdc-tab-indicator", "mdc-tab-indicator--active");
        }

        [Fact]
        public void HtmlStructure_TabComponents_TabIndicator_Content()
        {
            var sut = host.AddComponent<TabBarWithSingleTab>();

            var rootNode = sut.GetDocumentNode();
            var tabIndicatorContent = rootNode.SelectNodes("/div/div/div/div/button/span[2]/span").ShouldHaveSingleItem();
            tabIndicatorContent.ShouldContainCssClasses("mdc-tab-indicator__content", "mdc-tab-indicator__content--underline");
        }

        [Fact]
        public void HtmlStructure_TabComponents_TabRipple()
        {
            var sut = host.AddComponent<TabBarWithSingleTab>();

            var rootNode = sut.GetDocumentNode();
            var ripple = rootNode.SelectNodes("/div/div/div/div/button/span[3]").ShouldHaveSingleItem();
            ripple.ShouldContainCssClasses("mdc-tab__ripple");
        }
    }
}
