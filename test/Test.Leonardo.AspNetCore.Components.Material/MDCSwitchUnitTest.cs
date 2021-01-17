using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Switch;
using Microsoft.JSInterop;
using Shouldly;
using Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCSwitchUnitTest : MaterialComponentUnitTest<MDCSwitch>
    {
        public MDCSwitchUnitTest()
        {
            host.AddService<IJSRuntime, JSRuntimeFake>(new JSRuntimeFake(switchJsInterop));
        }

        private readonly MDCSwitchJsInteropFake switchJsInterop = new MDCSwitchJsInteropFake();

        [Fact]
        public void HtmlStructure_MdcSwitch()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div").ShouldHaveSingleItem();

            divElement.ShouldContainCssClasses("mdc-switch");
        }

        [Fact]
        public void HtmlStructure_MdcSwitchTrack()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div/div[1]").ShouldHaveSingleItem();

            divElement.ShouldContainCssClasses("mdc-switch__track");
        }

        [Fact]
        public void HtmlStructure_MdcSwitchThumbUnderlay()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div/div[2]").ShouldHaveSingleItem();

            divElement.ShouldContainCssClasses("mdc-switch__thumb-underlay");
        }

        [Fact]
        public void HtmlStructure_MdcSwitchThumb()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div/div[2]/div").ShouldHaveSingleItem();

            divElement.ShouldContainCssClasses("mdc-switch__thumb");
        }

        [Fact]
        public void HtmlStructure_MdcSwitchCheckbox_Type()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var inputElement = rootNode.SelectNodes("/div/div[2]/input").ShouldHaveSingleItem();

            var type = inputElement.Attributes["type"];
            type.Value.ShouldBe("checkbox");
        }

        [Fact]
        public void HtmlStructure_MdcSwitchCheckbox_Class()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var inputElement = rootNode.SelectNodes("/div/div[2]/input").ShouldHaveSingleItem();

            var cssClass = inputElement.Attributes["class"];
            cssClass.Value.ShouldBe("mdc-switch__native-control");
        }

        [Fact]
        public void HtmlStructure_MdcSwitchCheckbox_Role()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var inputElement = rootNode.SelectNodes("/div/div[2]/input").ShouldHaveSingleItem();

            var role = inputElement.Attributes["role"];
            role.Value.ShouldBe("switch");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_Label(string label)
        {
            var sut = AddComponent(("Label", label));

            var rootNode = sut.GetDocumentNode();
            var labelElement = rootNode.SelectNodes("/label").ShouldHaveSingleItem();

            labelElement.InnerText.ShouldBe(label);
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_LabelIdMatchesInputId(string label)
        {
            var sut = AddComponent(("Label", label));

            var rootNode = sut.GetDocumentNode();
            var labelElement = rootNode.SelectNodes("/label").ShouldHaveSingleItem();
            var inputElement = rootNode.SelectNodes("/div/div[2]/input").ShouldHaveSingleItem();

            var inputId = inputElement.Attributes["id"].Value;
            inputId.ShouldNotBeNullOrEmpty();

            labelElement.Attributes["for"].Value.ShouldBe(inputId);
        }
    }
}
