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
        public void GivenDataSourceAndValue_WhenFirstRendered_OptionIsPreSelected(List<Season> dataSource)
        {
            var preSelectedValue = default(Season);

            var sut = AddComponent(
                ("DataSource", dataSource));

            jsMock.Verify(
                r => r.InvokeAsync<object>("MDCSelectComponent.setSelectedIndex", It.Is<object[]>(s => (int)s[1] == 0)));

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
