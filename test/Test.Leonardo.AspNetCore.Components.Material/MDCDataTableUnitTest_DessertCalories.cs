using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.DataTable;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Shouldly;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;
using static Test.Leonardo.AspNetCore.Components.Material.MDCDataTableUnitTest_DessertCalories;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCDataTableUnitTest_DessertCalories : MDCDataTableUnitTest<DessertCalories>
    {
        [Theory]
        [AutoData]
        public void HtmlStructure_MdcDataTable_Table_HeaderRow(DessertCalories item)
        {
            var sut = AddComponent(("DataSource", new[] { item }));

            var rootNode = sut.GetDocumentNode();
            var rowElement = rootNode.SelectNodes("//div/table/thead/tr").ShouldHaveSingleItem();
            rowElement.ShouldContainCssClasses("mdc-data-table__header-row");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcDataTable_Table_Body_SingleRow(DessertCalories item)
        {
            var sut = AddComponent(("DataSource", new[] { item }));

            var rootNode = sut.GetDocumentNode();
            var rowElement = rootNode.SelectNodes("//div/table/tbody/tr").ShouldHaveSingleItem();
            rowElement.ShouldContainCssClasses("mdc-data-table__row");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcDataTable_Table_HeaderCell(DessertCalories item)
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
        public void HtmlStructure_MdcDataTable_Table_HeaderCell_Value(DessertCalories item, string header)
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
        public void HtmlStructure_MdcDataTable_Table_HeaderCell_Role(DessertCalories item)
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
        public void HtmlStructure_MdcDataTable_Table_HeaderCell_Scope(DessertCalories item)
        {
            var sut = AddComponent(
                ("DataSource", new[] { item }),
                ("ChildContent", (RenderFragment)(b => BuildMDCDrawerNavLinkRenderFragment(b, new MDCDataTableColumnData()))));

            var rootNode = sut.GetDocumentNode();
            var scope = rootNode.SelectNodes("//div/table/thead/tr/th").ShouldHaveSingleItem().Attributes["scope"];
            scope.ShouldNotBeNull();
            scope.Value.ShouldBe("col");
        }

        private static void BuildMDCDrawerNavLinkRenderFragment(RenderTreeBuilder b, params MDCDataTableColumnData[] navLinks)
        {
            int c = 0;
            foreach (var item in navLinks)
            {
                b.OpenComponent<MDCDataTableColumn>(c++);
                b.AddAttribute(c++, "Header", item.Header);
                b.CloseComponent();
            }
        }

        public class DessertCalories
        {
        }

        private class MDCDataTableColumnData
        {
            public string Header { get; set; }
        }
    }
}
