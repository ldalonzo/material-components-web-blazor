using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Testing.TabBar;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.JSInterop;
using Shouldly;
using Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCTabBarWithThreeTabsUnitTest
    {
        public MDCTabBarWithThreeTabsUnitTest()
        {
            host = new TestHost();

            host.AddService<IJSRuntime, JSRuntimeFake>(new JSRuntimeFake(mdTabBarJsInterop));
        }

        private readonly TestHost host;
        private readonly MDCTabBarJsInteropFake mdTabBarJsInterop = new MDCTabBarJsInteropFake();

        [Fact]
        public void FirstTabIsActive()
        {
            var sut = host.AddComponent<TabBarWithThreeTabs>();

            var rootNode = sut.GetDocumentNode();
            var tabElements = rootNode.SelectNodes("/div/div/div/div/button");

            tabElements.Count.ShouldBe(3);
            tabElements[0].ShouldContainCssClasses("mdc-tab", "mdc-tab--active");
            tabElements[1].ShouldContainCssClasses("mdc-tab");
            tabElements[2].ShouldContainCssClasses("mdc-tab");
        }

        [Theory]
        [AutoData]
        public void ActivateTab(string id)
        {
            var sut = host.AddComponent<TabBarWithThreeTabs>(("Id", id));

            var jsFake = mdTabBarJsInterop.FindComponentById(id);
            host.WaitForNextRender(() => jsFake.SetActiveTab(1));

            var rootNode = sut.GetDocumentNode();
            var tabElements = rootNode.SelectNodes("/div/div/div/div/button");

            tabElements.Count.ShouldBe(3);
            tabElements[0].ShouldContainCssClasses("mdc-tab");
            tabElements[1].ShouldContainCssClasses("mdc-tab", "mdc-tab--active");
            tabElements[2].ShouldContainCssClasses("mdc-tab");
        }
    }
}
