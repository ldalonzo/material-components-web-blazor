﻿using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.TopAppBar;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Moq;
using Shouldly;
using System.Linq;
using System.Threading.Tasks;
using Test.Blazor.Material.Components;
using Test.Leonardo.AspNetCore.Components.Material.Framework;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCTopAppBarUnitTest : MaterialComponentUnitTest<MDCTopAppBar>
    {
        public MDCTopAppBarUnitTest()
        {
            jsInterop = new MDCTopAppBarJsInteropFake();
            host.AddService<IJSRuntime, JSRuntimeFake>(new JSRuntimeFake(jsInterop));
        }

        private readonly MDCTopAppBarJsInteropFake jsInterop;

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
        public async Task OnNav_IsCalled()
        {
            var observer = new Mock<Spy>();
            var sut = AddComponent(("OnNav", EventCallback.Factory.Create(this, observer.Object.Call)));

            var jsComponent = jsInterop.FindComponentById(sut.Instance.ElementId);
            await jsComponent.Emit("MDCTopAppBar:nav");

            observer.Verify(o => o.Call(), Times.Once);
        }
    }
}
