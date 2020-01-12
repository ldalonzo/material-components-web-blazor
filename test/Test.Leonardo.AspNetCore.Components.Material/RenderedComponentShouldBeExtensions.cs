using HtmlAgilityPack;
using Leonardo.AspNetCore.Components.Material.Select;
using Microsoft.AspNetCore.Components.Testing;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Test.Leonardo.AspNetCore.Components.Material;

namespace Test.Blazor.Material.Components
{
    public static class RenderedComponentShouldBeExtensions
    {
        public static IEnumerable<string> GetCssClasses(this HtmlNode target)
            => target.Attributes["class"].Value.Split();

        public static void ShouldContainCssClasses(this HtmlNode target, params string[] expected)
            => target.GetCssClasses().ShouldBe(expected.AsEnumerable(), ignoreOrder: true);

        public static void DropdownShouldHaveSingleSelectedItem<T>(this RenderedComponent<MDCSelect<T>> sut, string selectedItemDataValue) => sut
            .FindListItemNodes()
            .Where(n => n.GetCssClasses().Contains("mdc-list-item--selected"))
            .ShouldHaveSingleItem()
            .Attributes["data-value"].Value.ShouldBe(selectedItemDataValue);

        public static void LabelShouldFloatAbove<T>(this RenderedComponent<MDCSelect<T>> sut) => sut
            .FindFloatingLabelNode()
            .ShouldContainCssClasses("mdc-floating-label", "mdc-floating-label--float-above");

        public static void DataValueAttributeShouldBePresentOnEachOption<T>(this RenderedComponent<MDCSelect<T>> sut, IEnumerable<T> expectedDataSource, bool includeEmpty)
        {
            var optionNodes = sut.FindListItemNodes();

            optionNodes
                .Where(r => r.Attributes["data-value"] != null)
                .Count()
                .ShouldBe(expectedDataSource.Count() + (includeEmpty ? 1 : 0));

            optionNodes
                .Select(r => r.Attributes["data-value"].Value)
                .ShouldBeUnique();
        }

        public static void SelectedTextShouldBe<T>(this RenderedComponent<MDCSelect<T>> sut, string expectedDisplayText)
        {
            var selectedText = sut.FindSelectedTextNode();
            selectedText.ShouldContainCssClasses("mdc-select__selected-text");
            selectedText.InnerText.ShouldBe(expectedDisplayText);
        }
    }
}
