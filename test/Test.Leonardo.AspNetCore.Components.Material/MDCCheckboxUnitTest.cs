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
using Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop;

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
    }
}
