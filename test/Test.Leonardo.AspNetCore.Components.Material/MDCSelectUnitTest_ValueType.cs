using System.Collections.Generic;
using AutoFixture.Xunit2;
using Shouldly;
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
            var sut = AddComponent(
                ("Id", id),
                ("DataSource", dataSource));

            selectJsInterop.FindComponentById(id).SelectedIndex.ShouldBe(0);
            sut.Instance.Value.ShouldBe(default);

            sut.DataValueAttributeShouldBePresentOnEachOption(dataSource, includeEmpty: false);
        }

        [Theory]
        [AutoData]
        public void PreFilled_LabelShouldFloat(List<Season> dataSource, string label)
        {
            var sut = AddComponent(
                ("Label", label),
                ("DataSource", dataSource));

            var rootNode = sut.GetDocumentNode();
            var labelElement = rootNode.SelectNodes("/div/div[1]/span[2]").ShouldHaveSingleItem();
            labelElement.Attributes["class"].Value.Split(" ").ShouldBe(new[] { "mdc-floating-label", "mdc-floating-label--float-above" });

            labelElement.InnerText.ShouldBe(label);
        }

        [Theory]
        [AutoData]
        public void OptionIsPreSelected(List<Season> dataSource)
        {
            var preSelectedValue = default(Season);

            var sut = AddComponent(("DataSource", dataSource));

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
