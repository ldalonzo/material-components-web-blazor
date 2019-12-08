using AutoFixture.Xunit2;
using HtmlAgilityPack;
using Leonardo.AspNetCore.Components.Material.Select;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.JSInterop;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Blazor.Material.Components;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCSelectUnitTest : MaterialComponentUnitTest<MDCSelect>
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

            var selectListItems = GetListItems(select);

            var emptyItemNode = selectListItems.ShouldHaveSingleItem();
            emptyItemNode.Attributes["data-value"].Value.ShouldBeNullOrEmpty();
        }

        [Theory]
        [AutoData]
        public void WithDataSource_DropDown_ContainsAllItems(List<FoodGroup> dataSource)
        {
            var select = AddComponent(("DataSource", dataSource));

            var selectListItems = GetListItems(select);
            selectListItems.Count().ShouldBe(dataSource.Count + 1);
        }

        [Fact]
        public void JavaScriptInstantiation()
        {
            var textField = AddComponent();

            jsMock.Verify(
                r => r.InvokeAsync<object>("MDCSelectComponent.attachTo", It.IsAny<object[]>()),
                Times.Once);
        }

        public static bool MatchAttachToArguments(object[] args)
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

        private static HtmlNodeCollection GetListItems(RenderedComponent<MDCSelect> source)
            => source.Find("div").SelectNodes("div/ul/li");

        public class FoodGroup
        {

        }
    }
}
