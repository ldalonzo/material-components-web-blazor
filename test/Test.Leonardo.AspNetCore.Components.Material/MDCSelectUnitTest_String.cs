using System.Collections.Generic;
using System.Linq;
using AutoFixture.Xunit2;
using Shouldly;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCSelectUnitTest_String : MDCSelectUnitTest<string>
    {
        [Theory]
        [AutoData]
        public void GivenDataSource_DropDown_ContainsAllItems(List<string> dataSource)
        {
            var sut = AddComponent(("DataSource", dataSource));

            sut.DataValueAttributeShouldBePresentOnEachOption(dataSource, includeEmpty: true);

            var selectListItems = sut.FindListItemNodes();

            // The first item should be the 'empty' item;
            var firstItem = selectListItems.First();
            firstItem.Attributes["data-value"].Value.ShouldBeNullOrEmpty();
            firstItem.InnerText.ShouldBeNullOrWhiteSpace();

            var actualItems = selectListItems.Skip(1);

            // There must be N items
            actualItems.ShouldNotBeEmpty();
            actualItems.Count().ShouldBe(dataSource.Count);

            // The display text for each option should match what we specified in the data source
            actualItems.Select(r => r.InnerText.Trim()).ShouldBe(dataSource);
        }
    }
}
