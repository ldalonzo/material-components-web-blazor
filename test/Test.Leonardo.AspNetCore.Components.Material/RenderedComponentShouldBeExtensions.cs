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

        public static void ShouldContainSelectedItemInMenu<T>(this RenderedComponent<MDCSelect<T>> source, string selectedItemDataValue) => source
            .FindListItemNodes()
            .Where(n => n.Attributes["data-value"].Value == selectedItemDataValue)
            .ShouldHaveSingleItem()
            .ShouldContainCssClasses("mdc-list-item", "mdc-list-item--selected");
    }
}
