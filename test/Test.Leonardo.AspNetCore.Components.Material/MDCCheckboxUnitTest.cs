using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Checkbox;
using Shouldly;
using Test.Blazor.Material.Components;
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

            var labelNode = sut.Find("div").SelectSingleNode("label");
            labelNode.ShouldNotBeNull();
            labelNode.InnerText.ShouldBe(label);
        }
    }
}
