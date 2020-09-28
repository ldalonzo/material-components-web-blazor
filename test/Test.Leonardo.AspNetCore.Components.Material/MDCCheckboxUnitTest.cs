using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Checkbox;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.JSInterop;
using Shouldly;
using Test.Leonardo.AspNetCore.Components.Material.Framework;
using Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCCheckboxUnitTest : MaterialComponentUnitTest<MDCCheckbox>
    {
        public MDCCheckboxUnitTest()
        {
            host.AddService<IJSRuntime, JSRuntimeFake>(new JSRuntimeFake(checkboxJsInterop));
        }

        private readonly MDCCheckboxJsInteropFake checkboxJsInterop = new MDCCheckboxJsInteropFake();

        [Fact]
        public void HtmlStructure_MdcCheckbox()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div/div").ShouldHaveSingleItem();

            divElement.ShouldContainCssClasses("mdc-checkbox");
        }

        [Fact]
        public void HtmlStructure_MdcCheckbox_Disabled()
        {
            var sut = AddComponent(("Disabled", true));

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div/div").ShouldHaveSingleItem();

            divElement.ShouldContainCssClasses("mdc-checkbox", "mdc-checkbox--disabled");
        }

        [Fact]
        public void HtmlStructure_MdcCheckbox_HasId()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div/div").ShouldHaveSingleItem();

            divElement.Attributes["id"].Value.ShouldNotBeNullOrEmpty();
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcCheckbox_HasId_Assigned(string id)
        {
            var sut = AddComponent(("Id", id));

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div/div").ShouldHaveSingleItem();

            divElement.Attributes["id"].Value.ShouldBe(id);
        }

        [Fact]
        public void HtmlStructure_MdcCheckbox_Input()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var inputNode = rootNode.SelectNodes("/div/div/input").ShouldHaveSingleItem();

            inputNode.ShouldContainCssClasses("mdc-checkbox__native-control");
        }

        [Fact]
        public void HtmlStructure_MdcCheckbox_Input_HasId()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div/div/input").ShouldHaveSingleItem();

            divElement.Attributes["id"].Value.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public void HtmlStructure_MdcCheckbox_Input_Disabled()
        {
            var sut = AddComponent(("Disabled", true));

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div/div/input").ShouldHaveSingleItem();

            divElement.Attributes["disabled"].ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public void Label_IsRendered(string label)
        {
            var sut = AddComponent(("Label", label));
            sut.ShouldHaveLabelNode().InnerText.ShouldBe(label);
        }

        [Fact]
        public void Label_IsLinkedToInput()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var inputNode = rootNode.SelectNodes("/div/div/input").ShouldHaveSingleItem();

            var inputId = inputNode.Attributes["id"].Value;
            inputId.ShouldNotBeNullOrEmpty();

            var targetId = sut.ShouldHaveLabelNode().Attributes["for"].Value;
            targetId.ShouldNotBeNullOrEmpty();

            inputId.ShouldBe(targetId);
        }

        [Fact]
        public void DifferentComponentHaveDifferentIds()
        {
            var sut1 = AddComponent();
            var sut2 = AddComponent();

            var id1 = sut1.GetDocumentNode().SelectNodes("/div/div/input").ShouldHaveSingleItem().Attributes["id"].Value;
            var id2 = sut2.GetDocumentNode().SelectNodes("/div/div/input").ShouldHaveSingleItem().Attributes["id"].Value;

            id1.ShouldNotBe(id2);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ChainedBind_ToggleValue(bool value)
        {
            var spy = new ValueSpy<bool>(value);

            var sut = AddComponent(
                ("Value", value),
                ("ValueChanged", EventCallback.Factory.Create<bool>(this, spy.SetValue)));

            await sut.Find("input").InputAsync(!value);

            spy.Value.ShouldBe(!value);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task ChainedBind_ToggleValue_ClearsIndeterminate(bool value)
        {
            var spy = new ValueSpy<bool>(true);

            var sut = AddComponent(
                ("Indeterminate", true),
                ("IndeterminateChanged", EventCallback.Factory.Create<bool>(this, spy.SetValue)),
                ("Value", value));

            await sut.Find("input").InputAsync(!value);

            spy.Value.ShouldBe(false);
        }

        [Theory]
        [AutoData]
        public void JavaScriptInstantiation_AttachTo(string id)
        {
            AddComponent(("Id", id));

            checkboxJsInterop.FindComponentById(id).ShouldNotBeNull();
        }

        [Theory]
        [InlineAutoData(true)]
        [InlineAutoData(false)]
        public void JavaScriptInstantiation_SetChecked(bool value, string id)
        {
            AddComponent(
                ("Id", id),
                ("Value", value));

            checkboxJsInterop.FindComponentById(id).Checked.ShouldBe(value);
        }

        [Theory]
        [InlineAutoData(true)]
        [InlineAutoData(false)]
        public void JavaScriptInstantiation_Indeterminate(string id, bool value)
        {
            AddComponent(
                ("Id", id),
                ("Indeterminate", value));

            checkboxJsInterop.FindComponentById(id).Indeterminate.ShouldBe(value);
        }
    }
}
