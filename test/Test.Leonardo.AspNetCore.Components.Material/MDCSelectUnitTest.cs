﻿using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Select;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Moq;
using Shouldly;
using System.Threading.Tasks;
using Test.Blazor.Material.Components;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public abstract class MDCSelectUnitTest<T> : MaterialComponentUnitTest<MDCSelect<T>>
    {
        public MDCSelectUnitTest()
        {
            jsMock = new Mock<IJSRuntime>(MockBehavior.Strict);

            jsMock
                .Setup(r => r.InvokeAsync<object>(
                    It.Is<string>(identifier => identifier == "MDCSelectComponent.attachTo"),
                    It.Is<object[]>(args => MatchAttachToArguments(args))))
                .Returns(new ValueTask<object>())
                .Verifiable();

            host.AddService(jsMock.Object);
        }

        private readonly Mock<IJSRuntime> jsMock;

        [Fact]
        public void Style_HasMandatoryCssClasses()
        {
            var select = AddComponent();

            select.GetCssClassesForElement("div").ShouldBe(new[] { "mdc-select" });
        }

        [Theory]
        [AutoData]
        public void Label_IsRendered(string label)
        {
            var select = AddComponent(("Label", label));

            // ASSERT the label is present in the markup
            select.GetMarkup().ShouldContain(label);

            // ASSERT the label is in the right place.
            var floatingLabelNode = select.Find("div").SelectSingleNode("div/span");
            floatingLabelNode.ShouldNotBeNull();
            floatingLabelNode.ChildNodes.ShouldNotBeEmpty();
            floatingLabelNode.ChildNodes.ShouldHaveSingleItem().InnerText.ShouldBe(label);
        }

        [Fact]
        public void Dropdown_IsRendered()
        {
            var select = AddComponent();

            var selectListNode = select.Find("div").SelectSingleNode("div/ul");
            selectListNode.Attributes["class"].Value.Split().ShouldBe(new[] { "mdc-list" });
        }

        [Fact]
        public void DropDown_HasEmptyItem()
        {
            var select = AddComponent();

            var selectListItems = select.GetListItems();

            var emptyItemNode = selectListItems.ShouldHaveSingleItem();
            emptyItemNode.Attributes["data-value"].Value.ShouldBeNullOrEmpty();
        }

        [Fact]
        public void JavaScriptInstantiation()
        {
            var textField = AddComponent();

            jsMock.Verify(
                r => r.InvokeAsync<object>("MDCSelectComponent.attachTo", It.IsAny<object[]>()),
                Times.Once);
        }

        private static bool MatchAttachToArguments(object[] args)
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
