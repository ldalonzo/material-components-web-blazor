using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.DataTable;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Shouldly;
using System;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;
using static Test.Leonardo.AspNetCore.Components.Material.MDCDataTableUnitTest_WeatherForecast;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCDataTableUnitTest_WeatherForecast : MDCDataTableUnitTest<WeatherForecast>
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
        public void HtmlStructure_MdcDataTable_Table_HeaderCell(WeatherForecast item)
        {
            var sut = AddComponent(
                ("DataSource", new[] { item }),
                ("ChildContent", (RenderFragment)(b => BuildMDCDrawerNavLinkRenderFragment(b, new MDCDataTableColumnData()))));

            var rootNode = sut.GetDocumentNode();
            var columnHeaderElement = rootNode.SelectNodes("//div/table/thead/tr/th").ShouldHaveSingleItem();
            columnHeaderElement.ShouldContainCssClasses("mdc-data-table__header-cell");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcDataTable_Table_HeaderCell_Value(WeatherForecast item, string header)
        {
            var sut = AddComponent(
                ("DataSource", new[] { item }),
                ("ChildContent", (RenderFragment)(b => BuildMDCDrawerNavLinkRenderFragment(b, new MDCDataTableColumnData { Header = header}))));

            var rootNode = sut.GetDocumentNode();
            var columnHeaderElement = rootNode.SelectNodes("//div/table/thead/tr/th").ShouldHaveSingleItem();
            columnHeaderElement.InnerText.ShouldBe(header);
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcDataTable_Table_HeaderCell_Role(WeatherForecast item)
        {
            var sut = AddComponent(
                ("DataSource", new[] { item }),
                ("ChildContent", (RenderFragment)(b => BuildMDCDrawerNavLinkRenderFragment(b, new MDCDataTableColumnData()))));

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
                ("ChildContent", (RenderFragment)(b => BuildMDCDrawerNavLinkRenderFragment(b, new MDCDataTableColumnData()))));

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
                ("ChildContent", (RenderFragment)(b => BuildMDCDrawerNavLinkRenderFragment(b, new MDCDataTableColumnData { DataMember = nameof(WeatherForecast.Summary) }))));

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
                ("ChildContent", (RenderFragment)(b => BuildMDCDrawerNavLinkRenderFragment(b, new MDCDataTableColumnData { DataMember = nameof(WeatherForecast.TemperatureC) }))));

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
                ("ChildContent", (RenderFragment)(b => BuildMDCDrawerNavLinkRenderFragment(b, new MDCDataTableColumnData { DataMember = nameof(WeatherForecast.Date) }))));

            var rootNode = sut.GetDocumentNode();
            var cellElement = rootNode.SelectNodes("//div/table/tbody/tr/td").ShouldHaveSingleItem();
            cellElement.InnerText.ShouldBe($"{item.Date}");
        }

        private static void BuildMDCDrawerNavLinkRenderFragment(RenderTreeBuilder b, params MDCDataTableColumnData[] navLinks)
        {
            int c = 0;
            foreach (var item in navLinks)
            {
                b.OpenComponent<MDCDataTableColumn>(c++);
                b.AddAttribute(c++, "Header", item.Header);
                b.AddAttribute(c++, "DataMember", item.DataMember);
                b.CloseComponent();
            }
        }

        public class WeatherForecast
        {
            public DateTime Date { get; set; }

            public int TemperatureC { get; set; }

            public string Summary { get; set; }

            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }

        private class MDCDataTableColumnData
        {
            public string Header { get; set; }
            public string DataMember { get; set; }
        }
    }
}
