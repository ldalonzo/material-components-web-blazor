using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Testing.DataTable;
using Microsoft.AspNetCore.Components.Testing;
using Shouldly;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCDataTableWeatherForecastDataTableUnitTest
    {
        public MDCDataTableWeatherForecastDataTableUnitTest()
        {
            host = new TestHost();
        }

        private readonly TestHost host;

        [Theory]
        [AutoData]
        public void DateTimeColumnWithTemplate(WeatherForecast item)
        {
            var sut = host.AddComponent<WeatherForecastDataTable>(("Forecasts", new[] { item }));

            var rootNode = sut.GetDocumentNode();
            var cellElement = rootNode.SelectNodes("//div/table/tbody/tr/td[1]").ShouldHaveSingleItem();
            cellElement.InnerText.Trim().ShouldBe($"{item.Date.ToShortDateString()}");
        }

        [Theory]
        [AutoData]
        public void NumericDataColumn(WeatherForecast item)
        {
            var sut = host.AddComponent<WeatherForecastDataTable>(("Forecasts", new[] { item }));

            var rootNode = sut.GetDocumentNode();

            var gcellElement = rootNode.SelectNodes("//div/table/thead/tr/th[2]").ShouldHaveSingleItem();
            gcellElement.ShouldContainCssClasses("mdc-data-table__header-cell", "mdc-data-table__header-cell--numeric");

            var cellElement = rootNode.SelectNodes("//div/table/tbody/tr/td[2]").ShouldHaveSingleItem();
            cellElement.ShouldContainCssClasses("mdc-data-table__cell", "mdc-data-table__cell--numeric");
        }
    }
}
