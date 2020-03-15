using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.DataTable;
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
            var divElement = rootNode.SelectNodes("//div").ShouldHaveSingleItem();
            divElement.ShouldContainCssClasses("mdc-data-table");
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
    }
}
