using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Testing.DataTable;
using Microsoft.AspNetCore.Components.Testing;
using Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCDataTableWithTemplatesUnitTest
    {
        public MDCDataTableWithTemplatesUnitTest()
        {
            host = new TestHost();
        }

        private readonly TestHost host;

        [Theory]
        [AutoData]
        public void ColumnWithTemplate(WeatherForecast item)
        {
            var sut = host.AddComponent<MDCDataTable_SingleColumnDateTimeWithTemplate>(("Forecasts", new[] { item }));

            var rootNode = sut.GetDocumentNode();
            var cellElement = rootNode.SelectNodes("//div/table/tbody/tr/td").ShouldHaveSingleItem();
            cellElement.InnerText.Trim().ShouldBe($"{item.Date.ToShortDateString()}");
        }
    }
}
