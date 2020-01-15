using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Checkbox;
using Shouldly;
using Test.Blazor.Material.Components;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCCheckboxUnitTest : MaterialComponentUnitTest<MDCCheckbox>
    {
        [Fact]
        public void Style_HasMandatoryCssClasses()
        {
            var sut = AddComponent();
            sut.Find("div").SelectSingleNode("div").ShouldContainCssClasses("mdc-checkbox");
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

            var inputNode = sut.Find("div").SelectSingleNode("div/input");
            inputNode.ShouldNotBeNull();
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
    }
}
