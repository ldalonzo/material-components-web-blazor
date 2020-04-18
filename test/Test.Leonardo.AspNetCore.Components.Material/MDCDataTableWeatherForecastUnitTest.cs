using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Testing.DataTable;
using Shouldly;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{

    public class MDCDataTableWeatherForecastUnitTest : MDCDataTableUnitTest<WeatherForecast>
    {
        [Theory]
        [AutoData]
        public void HtmlStructure_MdcDataTable_Table_HeaderRow(WeatherForecast item)
        {
            var sut = AddComponent(("DataSource", new[] { item }));

            var rootNode = sut.GetDocumentNode();
            var rowElement = rootNode.SelectNodes("//div/table/thead/tr").ShouldHaveSingleItem();
            rowElement.ShouldContainCssClasses("mdc-data-table__header-row");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcDataTable_Table_HeaderCell_Role(WeatherForecast item)
        {
            var sut = AddComponent(
                ("DataSource", new[] { item }),
                ("ChildContent", BuildMDCDataTableColumnsFragment(new MDCDataTableColumnData())));

            var rootNode = sut.GetDocumentNode();
            var roleAttribute = rootNode.SelectNodes("//div/table/thead/tr/th").ShouldHaveSingleItem().Attributes["role"];
            roleAttribute.ShouldNotBeNull();
            roleAttribute.Value.ShouldBe("columnheader");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcDataTable_Table_HeaderCell_Scope(WeatherForecast item)
        {
            var sut = AddComponent(
                ("DataSource", new[] { item }),
                ("ChildContent", BuildMDCDataTableColumnsFragment(new MDCDataTableColumnData())));

            var rootNode = sut.GetDocumentNode();
            var scope = rootNode.SelectNodes("//div/table/thead/tr/th").ShouldHaveSingleItem().Attributes["scope"];
            scope.ShouldNotBeNull();
            scope.Value.ShouldBe("col");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcDataTable_Table_Body_SingleRow(WeatherForecast item)
        {
            var sut = AddComponent(("DataSource", new[] { item }));

            var rootNode = sut.GetDocumentNode();
            var rowElement = rootNode.SelectNodes("//div/table/tbody/tr").ShouldHaveSingleItem();
            rowElement.ShouldContainCssClasses("mdc-data-table__row");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcDataTable_Table_Body_SingleRow_Column(WeatherForecast item)
        {
            var sut = AddComponent(
                ("DataSource", new[] { item }),
                ("ChildContent", BuildMDCDataTableColumnsFragment(new MDCDataTableColumnData { DataMember = nameof(WeatherForecast.Summary) })));

            var rootNode = sut.GetDocumentNode();
            var cellElement = rootNode.SelectNodes("//div/table/tbody/tr/td").ShouldHaveSingleItem();
            cellElement.ShouldContainCssClasses("mdc-data-table__cell");
            cellElement.InnerText.ShouldBe(item.Summary);
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcDataTable_Table_Body_SingleRow_ColumnNumeric(WeatherForecast item)
        {
            var sut = AddComponent(
                ("DataSource", new[] { item }),
                ("ChildContent", BuildMDCDataTableColumnsFragment(new MDCDataTableColumnData { DataMember = nameof(WeatherForecast.TemperatureC) })));

            var rootNode = sut.GetDocumentNode();
            var cellElement = rootNode.SelectNodes("//div/table/tbody/tr/td").ShouldHaveSingleItem();
            cellElement.InnerText.ShouldBe($"{item.TemperatureC}");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcDataTable_Table_Body_SingleRow_ColumnDateTime(WeatherForecast item)
        {
            var sut = AddComponent(
                ("DataSource", new[] { item }),
                ("ChildContent", BuildMDCDataTableColumnsFragment(new MDCDataTableColumnData { DataMember = nameof(WeatherForecast.Date) })));

            var rootNode = sut.GetDocumentNode();
            var cellElement = rootNode.SelectNodes("//div/table/tbody/tr/td").ShouldHaveSingleItem();
            cellElement.InnerText.ShouldBe($"{item.Date}");
        }
    }
}
