using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.DataTable;
using Microsoft.AspNetCore.Components;
using Shouldly;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public abstract class MDCDataTableUnitTest<T> : MaterialComponentUnitTest<MDCDataTable<T>>
    {
        [Fact]
        public void HtmlStructure_MdcDataTable()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div").ShouldHaveSingleItem();
            divElement.ShouldContainCssClasses("mdc-data-table");
        }

        [Theory]
        [InlineData(MDCDataTableDensity.Normal)]
        [InlineData(MDCDataTableDensity.Dense2)]
        [InlineData(MDCDataTableDensity.Dense4)]
        public void HtmlStructure_MdcDataTable_Density(MDCDataTableDensity density)
        {
            var sut = AddComponent(("Density", density));

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div").ShouldHaveSingleItem();

            switch (density)
            {
                case MDCDataTableDensity.Normal:
                    divElement.ShouldContainCssClasses("mdc-data-table");
                    break;
                case MDCDataTableDensity.Dense2:
                    divElement.ShouldContainCssClasses("mdc-data-table", "mdc-data-table--density-2");
                    break;
                case MDCDataTableDensity.Dense4:
                    divElement.ShouldContainCssClasses("mdc-data-table", "mdc-data-table--density-4");
                    break;
            }
        }

        [Fact]
        public void HtmlStructure_MdcDataTable_Table()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var tableElement = rootNode.SelectNodes("//div/table").ShouldHaveSingleItem();
            tableElement.ShouldContainCssClasses("mdc-data-table__table");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcDataTable_Table_Label(string label)
        {
            var sut = AddComponent(("Label", label));

            var rootNode = sut.GetDocumentNode();
            var tableElement = rootNode.SelectNodes("//div/table").ShouldHaveSingleItem();
            var labelAttribute = tableElement.Attributes["aria-label"].Value;
            labelAttribute.ShouldNotBeNull();
            labelAttribute.ShouldBe(label);
        }

        [Fact]
        public void HtmlStructure_MdcDataTable_Table_Head()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            rootNode.SelectNodes("//div/table/thead").ShouldHaveSingleItem().ShouldNotBeNull();
        }

        [Fact]
        public void HtmlStructure_MdcDataTable_Table_Body()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var bodyElement = rootNode.SelectNodes("//div/table/tbody").ShouldHaveSingleItem();
            bodyElement.ShouldContainCssClasses("mdc-data-table__content");
        }

        protected RenderFragment BuildMDCDataTableColumnsFragment(params MDCDataTableColumnData[] navLinks) => b =>
        {
            var sequence = 0;
            foreach (var item in navLinks)
            {
                b.OpenComponent<MDCDataTableColumn<T>>(sequence++);
                b.AddAttribute(sequence++, "Header", item.Header);
                b.AddAttribute(sequence++, "DataMember", item.DataMember);
                b.CloseComponent();
            }
        };

        protected class MDCDataTableColumnData
        {
            public string Header { get; set; }
            public string DataMember { get; set; }
        }
    }
}
