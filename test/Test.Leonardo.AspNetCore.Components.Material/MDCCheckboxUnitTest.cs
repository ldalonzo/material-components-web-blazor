using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Checkbox;
using Microsoft.AspNetCore.Components;
using Shouldly;
using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Microsoft.AspNetCore.Components.Testing;
using Xunit;
using Moq;
using Microsoft.JSInterop;
using Test.Leonardo.AspNetCore.Components.Material.Framework;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCCheckboxUnitTest : MaterialComponentUnitTest<MDCCheckbox>
    {
        public MDCCheckboxUnitTest()
        {
            jsMock = new Mock<IJSRuntime>(MockBehavior.Strict);

            jsMock
                .Setup(r => r.InvokeAsync<object>(
                    It.Is<string>(identifier => identifier == "MDCCheckboxComponent.attachTo"),
                    It.Is<object[]>(args => MatchArgs_AttachTo(args))))
                .Returns(new ValueTask<object>())
                .Verifiable();

            jsMock
                .Setup(r => r.InvokeAsync<object>(
                    It.Is<string>(i => i == "MDCCheckboxComponent.setChecked"),
                    It.Is<object[]>(a => MatchArgs_SetChecked(a))))
                .Returns(new ValueTask<object>())
                .Verifiable();

            host.AddService(jsMock.Object);
        }

        private readonly Mock<IJSRuntime> jsMock;

        [Fact]
        public void HasMandatoryCssClasses()
        {
            var sut = AddComponent();

            sut.ShouldHaveMdcCheckboxNode().ShouldContainCssClasses("mdc-checkbox");
        }

        [Fact]
        public void HasMandatoryCssClasses_Disabled()
        {
            var sut = AddComponent(("Disabled", true));

            sut.ShouldHaveMdcCheckboxNode().ShouldContainCssClasses("mdc-checkbox", "mdc-checkbox--disabled");
        }

        [Fact]
        public void Disabled()
        {
            var sut = AddComponent(("Disabled", true));

            sut.ShouldHaveMdcCheckboxNode().ShouldContainCssClasses("mdc-checkbox", "mdc-checkbox--disabled");

            var inputNode = sut.ShouldHaveInputNode();
            inputNode.Attributes["disabled"].ShouldNotBeNull();
        }

        [Fact]
        public void HasId()
        {
            var sut = AddComponent();
            sut.ShouldHaveMdcCheckboxNode().Attributes["id"].Value.ShouldNotBeNullOrEmpty();
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

            var inputNode = sut.ShouldHaveInputNode();
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

            var id1 = sut1.ShouldHaveInputNode().Attributes["id"].Value;
            var id2 = sut2.ShouldHaveInputNode().Attributes["id"].Value;

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

        [Fact]
        public void JavaScriptInstantiation_AttachTo()
        {
            var sut = AddComponent();

            jsMock.Verify(
                r => r.InvokeAsync<object>("MDCCheckboxComponent.attachTo", It.IsAny<object[]>()),
                Times.Once);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void JavaScriptInstantiation_SetChecked(bool value)
        {
            var sut = AddComponent(
                ("Value", value));

            jsMock.Verify(
                r => r.InvokeAsync<object>("MDCCheckboxComponent.setChecked", It.Is<object[]>(a => a.Length == 2 && Equals(a[1], value))),
                Times.Once);
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

        private static bool MatchArgs_SetChecked(object[] args)
        {
            if (args.Length != 2)
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
