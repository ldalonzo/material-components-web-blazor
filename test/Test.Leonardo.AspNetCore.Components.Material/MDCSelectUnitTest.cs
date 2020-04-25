using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Select;
using Microsoft.JSInterop;
using Shouldly;
using Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public abstract class MDCSelectUnitTest<T> : MaterialComponentUnitTest<MDCSelect<T>>
    {
        public MDCSelectUnitTest()
        {
            selectJsInterop = new MDCSelectJsInteropFake();
            host.AddService<IJSRuntime, JSRuntimeFake>(new JSRuntimeFake(selectJsInterop));
        }

        protected readonly MDCSelectJsInteropFake selectJsInterop;

        [Fact]
        public void MdcSelect_HasMandatoryCssClasses()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var rootElement = rootNode.SelectNodes("/div").ShouldHaveSingleItem();
            rootElement.ShouldContainCssClasses("mdc-select");
        }

        [Fact]
        public void MdcSelect_Anchor_HasMandatoryCssClasses()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var anchorElement = rootNode.SelectNodes("/div/div[1]").ShouldHaveSingleItem();
            anchorElement.ShouldContainCssClasses("mdc-select__anchor");
        }

        [Theory]
        [AutoData]
        public void MdcSelect_Label(string label)
        {
            var sut = AddComponent(("Label", label));

            var rootNode = sut.GetDocumentNode();
            var labelElement = rootNode.SelectNodes("/div/div[1]/span[2]").ShouldHaveSingleItem();
            labelElement.Attributes["class"].Value.Split(" ").ShouldContain("mdc-floating-label");

            labelElement.InnerText.ShouldBe(label);
        }

        [Fact]
        public void Dropdown_IsRendered()
        {
            var select = AddComponent();

            var selectListNode = select.Find("div").SelectSingleNode("div/ul");
            selectListNode.ShouldContainCssClasses("mdc-list");
        }

        [Theory]
        [AutoData]
        public void JavaScriptInstantiation(string id)
        {
            AddComponent(("Id", id));

            selectJsInterop.FindComponentById(id).ShouldNotBeNull();
        }
    }
}
