using Leonardo.AspNetCore.Components.Material.Drawer;
using Microsoft.JSInterop;
using Shouldly;
using Test.Leonardo.AspNetCore.Components.Material.Framework;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCDrawerUnitTest : MaterialComponentUnitTest<MDCDrawer>
    {
        public MDCDrawerUnitTest()
        {
            var jsInterop = new MDCDrawerJsInteropFake();
            host.AddService<IJSRuntime, JSRuntimeFake>(new JSRuntimeFake(jsInterop));
        }

        [Fact]
        public void HtmlStructure_MdcDrawer()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var asideElement = rootNode.SelectNodes("//aside").ShouldHaveSingleItem();
            asideElement.ShouldContainCssClasses("mdc-drawer");
        }

        [Fact]
        public void HtmlStructure_MdcDrawerContent()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var asideElement = rootNode.SelectNodes("//aside/div").ShouldHaveSingleItem();
            asideElement.ShouldContainCssClasses("mdc-drawer__content");
        }

        [Fact]
        public void HtmlStructure_Nav()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var asideElement = rootNode.SelectNodes("//aside/div/nav").ShouldHaveSingleItem();
            asideElement.ShouldContainCssClasses("mdc-list");
        }
    }
}
