using Leonardo.AspNetCore.Components.Material.Switch;
using Shouldly;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCSwitchUnitTest : MaterialComponentUnitTest<MDCSwitch>
    {
        [Fact]
        public void HtmlStructure_MdcSwitch()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div").ShouldHaveSingleItem();

            divElement.ShouldContainCssClasses("mdc-switch");
        }
    }
}
