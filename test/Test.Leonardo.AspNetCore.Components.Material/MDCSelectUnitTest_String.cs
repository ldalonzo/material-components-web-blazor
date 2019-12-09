using AutoFixture.Xunit2;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCSelectUnitTest_String : MDCSelectUnitTest<string>
    {
        [Theory]
        [AutoData]
        public void WithDataSource_DropDown_ContainsAllItems(List<string> dataSource)
        {
            var select = AddComponent(("DataSource", dataSource));

            var selectListItems = select.GetListItems();

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
            actualItems.Select(r => r.InnerText.Trim()).ShouldBe(dataSource);
        }
    }
}
