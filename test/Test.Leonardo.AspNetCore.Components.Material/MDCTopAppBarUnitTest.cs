using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.TopAppBar;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Moq;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Test.Blazor.Material.Components;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCTopAppBarUnitTest : MaterialComponentUnitTest<MDCTopAppBar>
    {
        public MDCTopAppBarUnitTest()
        {
            jsMock = new Mock<IJSRuntime>(MockBehavior.Strict);

            jsMock
                .Setup(r => r.InvokeAsync<object>(
                    It.Is<string>(identifier => identifier == "MDCTopAppBarComponent.attachTo"),
                    It.Is<object[]>(args => MatchArgs_AttachTo(args))))
                .Returns(new ValueTask<object>())
                .Verifiable();

            host.AddService(jsMock.Object);
        }

        private readonly Mock<IJSRuntime> jsMock;

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

        [Fact]
        public void JavaScriptInstantiation()
        {
            var textField = AddComponent();

            jsMock.Verify(
                r => r.InvokeAsync<object>("MDCTopAppBarComponent.attachTo", It.IsAny<object[]>()),
                Times.Once);
        }

        public static bool MatchArgs_AttachTo(object[] args)
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
    }
}
