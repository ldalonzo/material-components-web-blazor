using Leonardo.AspNetCore.Components.Material.TopAppBar;
using Test.Blazor.Material.Components;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCTopAppBarUnitTest : MaterialComponentUnitTest<MDCTopAppBar>
    {
        [Fact]
        public void HasMandatoryCssClasses()
        {
            var sut = AddComponent();

            sut.Find("header").ShouldContainCssClasses("mdc-top-app-bar");
        }
    }
}
