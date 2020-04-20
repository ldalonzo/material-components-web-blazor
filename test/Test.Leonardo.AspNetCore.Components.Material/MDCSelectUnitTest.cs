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
        public void Style_HasMandatoryCssClasses()
        {
            var select = AddComponent();
            select.Find("div").ShouldContainCssClasses("mdc-select");
        }

        [Theory]
        [AutoData]
        public void Label_IsRendered(string label)
        {
            var select = AddComponent(("Label", label));

            // ASSERT the label is present in the markup.
            select.GetMarkup().ShouldContain(label);

            // ASSERT the label is in the right place.
            var floatingLabelNode = select.FindFloatingLabelNode();
            floatingLabelNode.ShouldNotBeNull();
            floatingLabelNode.ChildNodes.ShouldNotBeEmpty();
            floatingLabelNode.ChildNodes.ShouldHaveSingleItem().InnerText.ShouldBe(label);
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
