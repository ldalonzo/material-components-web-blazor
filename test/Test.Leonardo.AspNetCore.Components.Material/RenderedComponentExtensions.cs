using HtmlAgilityPack;
using Leonardo.AspNetCore.Components.Material.Select;
using Microsoft.AspNetCore.Components.Testing;
using System.Collections.Generic;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public static class RenderedComponentExtensions
    {
        public static IEnumerable<HtmlNode> FindListItemNodes<T>(this RenderedComponent<MDCSelect<T>> source)
            => source.Find("div").SelectNodes("div/ul/li");

        public static HtmlNode FindFloatingLabelNode<T>(this RenderedComponent<MDCSelect<T>> source)
            => source.Find("div").SelectSingleNode("div/span");

        public static HtmlNode FindSelectedTextNode<T>(this RenderedComponent<MDCSelect<T>> source)
            => source.Find("div").SelectSingleNode("div/div");
    }
}
