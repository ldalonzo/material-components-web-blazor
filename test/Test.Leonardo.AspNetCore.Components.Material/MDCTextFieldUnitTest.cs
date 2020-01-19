using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.TextField;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.JSInterop;
using Moq;
using Shouldly;
using System.Threading.Tasks;
using Test.Blazor.Material.Components;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCTextFieldUnitTest : MaterialComponentUnitTest<MDCTextField>
    {
        public MDCTextFieldUnitTest()
        {
            jsMock = new Mock<IJSRuntime>(MockBehavior.Strict);

            jsMock
                .Setup(r => r.InvokeAsync<object>(
                    It.Is<string>(identifier => identifier == "MDCTextFieldComponent.attachTo"),
                    It.Is<object[]>(args => MatchArgs_AttachTo(args))))
                .Returns(new ValueTask<object>())
                .Verifiable();

            host.AddService(jsMock.Object);
        }

        private readonly Mock<IJSRuntime> jsMock;

        [Fact]
        public void Style_Filled_HasMandatoryCssClasses()
        {
            var sut = AddComponent(("Variant", MDCTextFieldStyle.Filled));
            sut.ShouldHaveMdcTextFieldNode().ShouldContainCssClasses("mdc-text-field");
            sut.ShouldHaveInputNode().Attributes["disabled"].ShouldBeNull();
        }

        [Fact]
        public void Style_Outlined_HasMandatoryCssClasses()
        {
            var sut = AddComponent(("Variant", MDCTextFieldStyle.Outlined));
            sut.ShouldHaveMdcTextFieldNode().ShouldContainCssClasses("mdc-text-field", "mdc-text-field--outlined");
        }

        [Theory]
        [InlineAutoData(MDCTextFieldStyle.Filled)]
        [InlineAutoData(MDCTextFieldStyle.Outlined)]
        public void Label_IsRendered(MDCTextFieldStyle variant, string label)
        {
            var textField = AddComponent(
                ("Variant", variant),
                ("Label", label));

            var labelNode = textField.Find("label");
            labelNode.ShouldNotBeNull();
            labelNode.ChildNodes.ShouldHaveSingleItem().InnerText.ShouldBe(label);
        }

        [Theory]
        [InlineAutoData(MDCTextFieldStyle.Filled)]
        [InlineAutoData(MDCTextFieldStyle.Outlined)]
        public void GivenTextFieldIsPreFilled_WhenFirstRendered_ThenLabelFloatsAbove(MDCTextFieldStyle variant, string label, string value)
        {
            var textField = AddComponent(
                ("Variant", variant),
                ("Label", label),
                ("Value", value));

            textField.Find("label").ShouldContainCssClasses("mdc-floating-label", "mdc-floating-label--float-above");
        }

        [Theory]
        [AutoData]
        public void GivenTextFieldIsPreFilledAndOutlined_WhenFirstRendered_ThenNotchedOutlineShouldHostLabel(string label, string value)
        {
            var textField = AddComponent(
                ("Variant", MDCTextFieldStyle.Outlined),
                ("Label", label),
                ("Value", value));

            var notchedOutlineNode = textField.Find("div").ChildNodes[3];
            notchedOutlineNode.ShouldNotBeNull();
            notchedOutlineNode.ShouldContainCssClasses("mdc-notched-outline", "mdc-notched-outline--notched");
        }

        [Theory]
        [InlineData(MDCTextFieldStyle.Filled)]
        [InlineData(MDCTextFieldStyle.Outlined)]
        public void Label_IsLinkedToInputElement(MDCTextFieldStyle variant)
        {
            var sut = AddComponent(("Variant", variant));

            var inputElement = sut.ShouldHaveInputNode();
            var labelElement = sut.Find("label");

            var inputId = inputElement.Attributes["id"];
            inputId.ShouldNotBeNull();
            inputId.Value.ShouldNotBeNullOrEmpty();

            var labelFor = labelElement.Attributes["for"];
            labelFor.ShouldNotBeNull();
            labelFor.Value.ShouldNotBeNullOrEmpty();

            inputId.Value.ShouldBe(labelFor.Value);
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
        [InlineData(MDCTextFieldStyle.Filled)]
        [InlineData(MDCTextFieldStyle.Outlined)]
        public void JavaScriptInstantiation(MDCTextFieldStyle variant)
        {
            var sut = AddComponent(("Variant", variant));

            jsMock.Verify(
                r => r.InvokeAsync<object>("MDCTextFieldComponent.attachTo", It.IsAny<object[]>()),
                Times.Once);
        }

        [Fact]
        public void Disabled()
        {
            var sut = AddComponent(("Disabled", true));

            sut.ShouldHaveMdcTextFieldNode().ShouldContainCssClasses("mdc-text-field", "mdc-text-field--disabled");
            sut.ShouldHaveInputNode().Attributes["disabled"].ShouldNotBeNull();
        }

        private static bool MatchArgs_AttachTo(object[] args)
        {
            if (args.Length != 1)
            {
                return false;
            }

            if (args[0].GetType() != typeof(ElementReference))
            {
                return false;
            }

            var elementReference = (ElementReference)args[0];
            if (string.IsNullOrEmpty(elementReference.Id))
            {
                return false;
            }

            return true;
        }
    }
}
