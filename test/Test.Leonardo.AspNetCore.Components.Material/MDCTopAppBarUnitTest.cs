using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.TopAppBar;
using Shouldly;
using System.Linq;
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

        [Theory]
        [AutoData]
        public void Title_IsRendered(string title)
        {
            var sut = AddComponent(("Title", title));

            sut.GetMarkup().ShouldContain(title);
            var titleNode = sut.Find("header").SelectNodes("div/section/span").FirstOrDefault();
            titleNode.ShouldNotBeNull();
            titleNode.ShouldContainCssClasses("mdc-top-app-bar__title");
            titleNode.InnerText.ShouldBe(title);
        }
    }
}
