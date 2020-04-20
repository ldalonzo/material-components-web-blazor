using AutoFixture.Xunit2;
using Moq;
using Shouldly;
using System.Collections.Generic;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;
using static Test.Leonardo.AspNetCore.Components.Material.MDCSelectUnitTest_ValueType;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCSelectUnitTest_ValueType : MDCSelectUnitTest<Season>
    {
        [Theory]
        [AutoData]
        public void GivenDataSourceAndValue_WhenFirstRendered_OptionIsPreSelected(string id, List<Season> dataSource)
        {
            var preSelectedValue = default(Season);

            var sut = AddComponent(
                ("Id", id),
                ("DataSource", dataSource));

            selectJsInterop.FindComponentById(id).SelectedIndex.ShouldBe(0);

            sut.Instance.Value.ShouldBe(default);

            sut.DataValueAttributeShouldBePresentOnEachOption(dataSource, includeEmpty: false);
            sut.LabelShouldFloatAbove();
            sut.SelectedTextShouldBe(default(Season).ToString());
            sut.DropdownShouldHaveSingleSelectedItem(preSelectedValue.ToString());
        }

        public enum Season
        {
            Spring,
            Summer,
            Autumn,
            Winter
        }
    }
}
