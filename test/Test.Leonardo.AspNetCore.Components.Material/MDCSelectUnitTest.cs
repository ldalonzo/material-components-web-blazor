using Leonardo.AspNetCore.Components.Material.Select;
using Shouldly;
using Test.Blazor.Material.Components;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCSelectUnitTest : MaterialComponentUnitTest<MDCSelect>
    {
        [Fact]
        public void Style_HasMandatoryCssClasses()
        {
            var select = AddComponent();

            select.GetCssClassesForElement("div").ShouldBe(new[] { "mdc-select" });
        }
    }
}
