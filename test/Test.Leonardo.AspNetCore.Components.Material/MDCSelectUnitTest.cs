using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Select;
using Shouldly;
using System.Linq;
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
        public void Menu_IsRendered()
        {
            var select = AddComponent();

            var selectListNode = select.Find("div").SelectSingleNode("div/ul");
            selectListNode.Attributes["class"].Value.Split().ShouldBe(new[] { "mdc-list" });
        }

        [Fact]
        public void Menu_HasEmptyItem()
        {
            var select = AddComponent();

            var selectListItems = select.Find("div").SelectNodes("div/ul/li");

            var emptyItemNode = selectListItems.ShouldHaveSingleItem();
            emptyItemNode.Attributes["data-value"].Value.ShouldBeNullOrEmpty();
        }
    }
}
