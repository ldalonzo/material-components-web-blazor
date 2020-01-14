using Leonardo.AspNetCore.Components.Material.Checkbox;
using Test.Blazor.Material.Components;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCCheckboxUnitTest : MaterialComponentUnitTest<MDCCheckbox>
    {
        [Fact]
        public void Style_HasMandatoryCssClasses()
        {
            var select = AddComponent();
            select.Find("div").SelectSingleNode("div").ShouldContainCssClasses("mdc-checkbox");
        }
    }
}
