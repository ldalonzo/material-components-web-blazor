using Leonardo.AspNetCore.Components.Material.TextField;
using Shouldly;
using System.Linq;
using Xunit;

namespace Test.Blazor.Material.Components
{
    public class MDCTextFieldUnitTest : MaterialComponentUnitTest<MDCTextField>
    {
        [Fact]
        public void TestMandatoryCssClass()
        {
            var component = AddComponent();
            component.GetCssClassForElement("div").ShouldContain("mdc-text-field");
        }
    }
}
