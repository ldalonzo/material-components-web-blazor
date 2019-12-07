using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.TextField;
using Microsoft.JSInterop;
using Moq;
using Shouldly;
using System.Threading.Tasks;
using Xunit;

namespace Test.Blazor.Material.Components
{
    public class MDCTextFieldUnitTest : MaterialComponentUnitTest<MDCTextField>
    {
        public MDCTextFieldUnitTest()
        {
            jsMock = new Mock<IJSRuntime>(MockBehavior.Strict);

            jsMock
                .Setup(r => r.InvokeAsync<object>("MDCTextFieldComponent.attachTo", It.IsAny<object[]>()))
                .Returns(new ValueTask<object>())
                .Verifiable();

            host.AddService(jsMock.Object);
        }

        private readonly Mock<IJSRuntime> jsMock;

        [Fact]
        public void Style_MandatoryCssClass()
        {
            var textField = AddComponent();
            textField.GetCssClassForElement("div").ShouldContain("mdc-text-field");
        }

        [Theory, AutoData]
        public void Label_IsRendered(string label)
        {
            var sut = AddComponent(("Label", label));

            sut.Find("label").ChildNodes.ShouldHaveSingleItem().InnerText.ShouldBe(label);
        }

        [Fact]
        public void Label_IsLinkedToInputElement()
        {
            var sut = AddComponent();

            var inputElement = sut.Find("input");
            var labelElement = sut.Find("label");

            var inputId = inputElement.Attributes["id"];
            inputId.ShouldNotBeNull();
            inputId.Value.ShouldNotBeNullOrEmpty();

            var labelFor = labelElement.Attributes["for"];
            labelFor.ShouldNotBeNull();
            labelFor.Value.ShouldNotBeNullOrEmpty();

            inputId.Value.ShouldBe(labelFor.Value);
        }

        [Fact]
        public void Label_IsLinkedToInputElement_AndDoNotClashWithOtherInstances()
        {
            var textField1 = AddComponent();
            var textField2 = AddComponent();

            var id1 = textField1.Find("input").Attributes["id"];
            var id2 = textField2.Find("input").Attributes["id"];

            id1.Value.ShouldNotBe(id2.Value);
        }

        [Theory, AutoData]
        public void Value_IsRendered(string value)
        {
            var textFied = AddComponent(("Value", value));
            textFied.Find("input").Attributes["value"].Value.ShouldBe(value);
        }

        [Fact]
        public void JavaScriptInstantiation()
        {
            var textField = AddComponent();

            jsMock.Verify(
                r => r.InvokeAsync<object>("MDCTextFieldComponent.attachTo", It.IsAny<object[]>()),
                Times.Once);
        }
    }
}
