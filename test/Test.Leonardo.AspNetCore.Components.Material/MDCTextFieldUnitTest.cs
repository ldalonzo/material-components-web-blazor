using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.TextField;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.JSInterop;
using Shouldly;
using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Framework;
using Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCTextFieldUnitTest : MaterialComponentUnitTest<MDCTextField>
    {
        public MDCTextFieldUnitTest()
        {
            FakeComponent = new MDCTextFieldJsInteropFake();
            host.AddService<IJSRuntime>(new JSRuntimeFake(FakeComponent));
        }

        private readonly MDCTextFieldJsInteropFake FakeComponent;

        [Fact]
        public void Filled_LabelNode_CssClasses()
        {
            var sut = AddComponent(("Variant", MDCTextFieldStyle.Filled));

            var rootNode = sut.GetDocumentNode();
            var labelElement = rootNode.SelectNodes("/label").ShouldHaveSingleItem();

            labelElement.ShouldContainCssClasses("mdc-text-field", "mdc-text-field--filled");
        }

        [Fact]
        public void Outlined_LabelNode_CssClasses()
        {
            var sut = AddComponent(("Variant", MDCTextFieldStyle.Outlined));

            var rootNode = sut.GetDocumentNode();
            var labelElement = rootNode.SelectNodes("/label").ShouldHaveSingleItem();

            labelElement.ShouldContainCssClasses("mdc-text-field", "mdc-text-field--outlined");
        }

        [Fact]
        public void Filled_RippleNode_Leading()
        {
            var sut = AddComponent(("Variant", MDCTextFieldStyle.Filled));

            var root = sut.GetDocumentNode();
            root.SelectNodes("//label/span[1]").ShouldHaveSingleItem().ShouldContainCssClasses("mdc-text-field__ripple");
        }

        [Fact]
        public void Filled_RippleNode_Trailing()
        {
            var sut = AddComponent(("Variant", MDCTextFieldStyle.Filled));

            var root = sut.GetDocumentNode();
            root.SelectNodes("//label/span[3]").ShouldHaveSingleItem().ShouldContainCssClasses("mdc-line-ripple");
        }

        [Theory]
        [InlineAutoData(MDCTextFieldStyle.Filled)]
        [InlineAutoData(MDCTextFieldStyle.Outlined)]
        public void Label_IsRendered(MDCTextFieldStyle variant, string label)
        {
            var sut = AddComponent(
                ("Variant", variant),
                ("Label", label));

            var labelNode = sut.ShouldHaveLabelNode(variant);
            labelNode.ChildNodes.ShouldHaveSingleItem().InnerText.ShouldBe(label);
        }

        [Theory]
        [InlineAutoData(MDCTextFieldStyle.Filled)]
        [InlineAutoData(MDCTextFieldStyle.Outlined)]
        public void GivenTextFieldIsPreFilled_WhenFirstRendered_ThenLabelFloatsAbove(MDCTextFieldStyle variant, string label, string value)
        {
            var sut = AddComponent(
                ("Variant", variant),
                ("Label", label),
                ("Value", value));

            sut.ShouldHaveLabelNode(variant).ShouldContainCssClasses("mdc-floating-label", "mdc-floating-label--float-above");
        }

        [Theory]
        [AutoData]
        public void Outlined_PreFilled_WhenFirstRendered_ThenNotchedOutlineShouldHostLabel(string label, string value)
        {
            var sut = AddComponent(
                ("Variant", MDCTextFieldStyle.Outlined),
                ("Label", label),
                ("Value", value));

            var rootNode = sut.GetDocumentNode();
            var notchedOutlineNode = rootNode.SelectNodes("//label/span[1]").ShouldHaveSingleItem();
            notchedOutlineNode.ShouldNotBeNull();
            notchedOutlineNode.ShouldContainCssClasses("mdc-notched-outline", "mdc-notched-outline--notched");
        }

        [Theory]
        [InlineData(MDCTextFieldStyle.Filled)]
        [InlineData(MDCTextFieldStyle.Outlined)]
        public void InputElement_HasAriaLabelledByAttribute(MDCTextFieldStyle variant)
        {
            var sut = AddComponent(("Variant", variant));

            var inputElement = sut.ShouldHaveInputNode();
            var inputId = inputElement.Attributes["aria-labelledby"];
            inputId.ShouldNotBeNull();
            inputId.Value.ShouldNotBeNullOrEmpty();

            var labelElement = sut.ShouldHaveLabelNode(variant);
            var labelElementId = labelElement.Attributes["id"];
            labelElementId.ShouldNotBeNull();
            labelElementId.Value.ShouldNotBeNullOrWhiteSpace();

            inputId.Value.ShouldBe(labelElementId.Value);
        }

        [Theory]
        [InlineData(MDCTextFieldStyle.Filled)]
        [InlineData(MDCTextFieldStyle.Outlined)]
        public void Label_IsLinkedToInputElement_AndDoNotClashWithOtherInstances(MDCTextFieldStyle variant)
        {
            var textField1 = AddComponent(("Variant", variant));
            var textField2 = AddComponent(("Variant", variant));

            var id1 = textField1.ShouldHaveInputNode().Attributes["id"];
            var id2 = textField2.ShouldHaveInputNode().Attributes["id"];

            id1.Value.ShouldNotBe(id2.Value);
        }

        [Theory]
        [InlineAutoData(MDCTextFieldStyle.Filled)]
        [InlineAutoData(MDCTextFieldStyle.Outlined)]
        public void Value_IsRendered(MDCTextFieldStyle variant, string value)
        {
            var textField = AddComponent(
                ("Value", value),
                ("Variant", variant));

            textField.ShouldHaveInputNode().Attributes["value"].Value.ShouldBe(value);
        }

        [Theory]
        [InlineAutoData(MDCTextFieldStyle.Filled)]
        [InlineAutoData(MDCTextFieldStyle.Outlined)]
        public async Task Value_DataBind(MDCTextFieldStyle variant, string value)
        {
            var spy = new ValueSpy<string>();

            var sut = AddComponent(
                ("Variant", variant),
                ("ValueChanged", EventCallback.Factory.Create<string>(this, spy.SetValue)));

            var inputNode = sut.ShouldHaveInputNode();
            inputNode.Attributes["value"].ShouldBeNull();

            await inputNode.InputAsync(value);
            sut.ShouldHaveInputNode().Attributes["value"].Value.ShouldBe(value);
            spy.Value.ShouldBe(value);
        }

        [Theory]
        [InlineAutoData(MDCTextFieldStyle.Filled)]
        [InlineAutoData(MDCTextFieldStyle.Outlined)]
        public void JavaScriptInstantiation(MDCTextFieldStyle variant, string id)
        {
            AddComponent(
                ("Id", id),
                ("Variant", variant));

            FakeComponent.FindComponentById(id).ShouldNotBeNull();
        }

        [Theory]
        [InlineData(MDCTextFieldStyle.Filled)]
        [InlineData(MDCTextFieldStyle.Outlined)]
        public void Disabled(MDCTextFieldStyle variant)
        {
            var sut = AddComponent(
                ("Variant", variant),
                ("Disabled", true));

            var rootNode = sut.GetDocumentNode();

            var labelElement = rootNode.SelectNodes("/label").ShouldHaveSingleItem();            
            labelElement.Attributes["class"].Value.Split(" ").ShouldContain("mdc-text-field--disabled");

            var inputElement = rootNode.SelectNodes("/label/input").ShouldHaveSingleItem();
            inputElement.Attributes["disabled"].ShouldNotBeNull();
        }
    }
}
