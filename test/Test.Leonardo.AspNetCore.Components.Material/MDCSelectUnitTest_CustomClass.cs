using AutoFixture;
using AutoFixture.Xunit2;
using Microsoft.JSInterop;
using Moq;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Blazor.Material.Components;
using Xunit;
using static Test.Leonardo.AspNetCore.Components.Material.MDCSelectUnitTest_CustomClass;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCSelectUnitTest_CustomClass : MDCSelectUnitTest<FoodGroup>
    {
        [Theory]
        [AutoData]
        public void GivenDataSource_WhenFirstRendered_DropDownContainsAllItems(List<FoodGroup> dataSource)
        {
            var select = AddComponent(
                ("DataSource", dataSource),
                ("DataValueMember", nameof(FoodGroup.Id)),
                ("DisplayTextMember", nameof(FoodGroup.DisplayText)));

            var selectListItems = select.FindListItemNodes();

            // The first item should be the 'empty' item;
            var firstItem = selectListItems.First();
            firstItem.Attributes["data-value"].Value.ShouldBeNullOrEmpty();
            firstItem.InnerText.ShouldBeNullOrWhiteSpace();

            // The data-value attribute must be unique across all options.
            selectListItems.Select(r => r.Attributes["data-value"].Value).ShouldBeUnique();

            var actualItems = selectListItems.Skip(1);

            // There must be N items
            actualItems.ShouldNotBeEmpty();
            actualItems.Count().ShouldBe(dataSource.Count);

            // The data-value attribute must be present on each option.
            actualItems.Select(r => r.Attributes["data-value"]).Count().ShouldBe(dataSource.Count);

            // The display text for each option should match what we specified in the data source
            actualItems.Select(r => r.InnerText.Trim()).ShouldBe(dataSource.Select(d => d.DisplayText));
        }

        [Theory]
        [AutoData]
        public void GivenDataSource_WhenFirstRendered_EmptyOptionIsSelected(List<FoodGroup> dataSource)
        {
            var select = AddComponent(
                ("DataSource", dataSource),
                ("DataValueMember", nameof(FoodGroup.Id)),
                ("DisplayTextMember", nameof(FoodGroup.DisplayText)));

            select.Instance.Value.ShouldBe(null);

            jsMock.Verify(
                r => r.InvokeAsync<object>("MDCSelectComponent.setSelectedIndex", It.Is<object[]>(s => (int)s[1] == 0)));

            var labelNode = select.FindFloatingLabelNode();
            labelNode.ShouldContainCssClasses("mdc-floating-label");
        }

        [Theory]
        [AutoData]
        public void GivenDataSourceAndValue_WhenFirstRendered_OptionIsPreSelected(List<FoodGroup> dataSource)
        {
            // GIVEN a select component that has a pre-selected value
            var fixture = new Fixture();
            var index = new Generator<int>(fixture).First(i => i >= 0 && i < dataSource.Count);
            var preSelectedValue = dataSource[index];

            var sut = AddComponent(
                ("DataSource", dataSource),
                ("Value", preSelectedValue),
                ("DataValueMember", nameof(FoodGroup.Id)),
                ("DisplayTextMember", nameof(FoodGroup.DisplayText)));

            jsMock.Verify(
                r => r.InvokeAsync<object>("MDCSelectComponent.setSelectedIndex", It.Is<object[]>(s => (int)s[1] == index + 1)));

            sut.Instance.Value.ShouldBe(preSelectedValue);

            // THEN the label node should float above
            var labelNode = sut.FindFloatingLabelNode();
            labelNode.ShouldContainCssClasses("mdc-floating-label", "mdc-floating-label--float-above");

            // THEN the selected item should be displayed
            var selectedText = sut.FindSelectedTextNode();
            selectedText.ShouldContainCssClasses("mdc-select__selected-text");
            selectedText.InnerText.ShouldBe(preSelectedValue.DisplayText);

            // THEN the selected item should be selected in the drop-down menu
            sut.ShouldContainSelectedItemInMenu(preSelectedValue.Id);
        }

        [Theory]
        [AutoData]
        public async Task GivenDataSource_WhenUserPicksItem_CurrentlySelectedOptionIsUpdated(List<FoodGroup> dataSource)
        {
            var select = AddComponent(
                ("DataSource", dataSource),
                ("DataValueMember", nameof(FoodGroup.Id)),
                ("DisplayTextMember", nameof(FoodGroup.DisplayText)));

            var fixture = new Fixture();
            var index = new Generator<int>(fixture).First(i => i >= 0 && i < dataSource.Count);
            var expectedSelection = dataSource[index];

            // ACT Simulate the user selecting a single option from the drop-down menu.
            await InvokeMethodAsync(DotNetObjectReference.Create(select.Instance), "OnChange", expectedSelection.Id, index);

            // ASSERT the selected value exposed as a property is what they chose.
            select.Instance.Value.ShouldBe(expectedSelection);
        }

        public class FoodGroup
        {
            public string Id { get; set; }
            public string DisplayText { get; set; }
        }
    }
}
