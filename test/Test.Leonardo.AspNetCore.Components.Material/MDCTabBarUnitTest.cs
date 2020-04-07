using Leonardo.AspNetCore.Components.Material.TabBar;
using Shouldly;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCTabBarUnitTest : MaterialComponentUnitTest<MDCTabBar>
    {
        [Fact]
        public void HtmlStructure_MdcTabBar()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode().FirstChild;
            var divElement = rootNode.SelectNodes("/div").ShouldHaveSingleItem();
            divElement.ShouldContainCssClasses("mdc-tab-bar");
        }

        [Fact]
        public void HtmlStructure_MdcTabBar_Role()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div").ShouldHaveSingleItem();

            var roleAttribute = divElement.Attributes["role"];
            roleAttribute.ShouldNotBeNull();
            roleAttribute.Value.ShouldBe("tablist");
        }

        [Fact]
        public void HtmlStructure_TabScroller()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var tabScroller = rootNode.SelectNodes("/div/div").ShouldHaveSingleItem();
            var tabScrollerClass = tabScroller.Attributes["class"];
            tabScrollerClass.ShouldNotBeNull();
            tabScrollerClass.Value.ShouldBe("mdc-tab-scroller");
        }

        [Fact]
        public void HtmlStructure_TabScroller_ScrollArea()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var tabScrollerArea = rootNode.SelectNodes("/div/div/div").ShouldHaveSingleItem();
            var tabScrollerAreaClass = tabScrollerArea.Attributes["class"];
            tabScrollerAreaClass.ShouldNotBeNull();
            tabScrollerAreaClass.Value.ShouldBe("mdc-tab-scroller__scroll-area");
        }

        [Fact]
        public void HtmlStructure_TabScroller_ScrollContent()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var tabScrollerContent = rootNode.SelectNodes("/div/div/div/div").ShouldHaveSingleItem();
            var tabScrollerContentClass = tabScrollerContent.Attributes["class"];
            tabScrollerContentClass.ShouldNotBeNull();
            tabScrollerContentClass.Value.ShouldBe("mdc-tab-scroller__scroll-content");
        }
    }
}
