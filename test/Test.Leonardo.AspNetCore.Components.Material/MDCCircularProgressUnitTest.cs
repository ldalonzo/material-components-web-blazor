using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.CircularProgress;
using Microsoft.JSInterop;
using Shouldly;
using Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCCircularProgressUnitTest : MaterialComponentUnitTest<MDCCircularProgress>
    {
        public MDCCircularProgressUnitTest()
        {
            host.AddService<IJSRuntime, JSRuntimeFake>(new JSRuntimeFake(jsInterop));
        }

        private readonly MDCCircularProgressJsInteropFake jsInterop = new MDCCircularProgressJsInteropFake();

        [Fact]
        public void HtmlStructure_MdcCircularProgress()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div").ShouldHaveSingleItem();
            divElement.ShouldContainCssClasses("mdc-circular-progress", "mdc-circular-progress--large");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcCircularProgress_Label(string label)
        {
            var sut = AddComponent(("Label", label));

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div").ShouldHaveSingleItem();
            var ariaLabelAttribute = divElement.Attributes["aria-label"];
            ariaLabelAttribute.ShouldNotBeNull();
            ariaLabelAttribute.Value.ShouldBe(label);
        }

        [Theory]
        [InlineAutoData(true)]
        [InlineAutoData(false)]
        public void Indeterminate(bool indeterminate, string id)
        {
            AddComponent(
                ("Id", id),
                ("Indeterminate", indeterminate));

            jsInterop.FindComponentById(id).IsDeterminate.ShouldBe(!indeterminate);
        }
    }
}
