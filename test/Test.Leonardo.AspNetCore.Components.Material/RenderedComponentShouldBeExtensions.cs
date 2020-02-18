using HtmlAgilityPack;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace Test.Blazor.Material.Components
{
    public static class RenderedComponentShouldBeExtensions
    {
        public static IEnumerable<string> GetCssClasses(this HtmlNode target)
            => target.Attributes["class"].Value.Split();

        public static void ShouldContainCssClasses(this HtmlNode target, params string[] expected)
            => target.GetCssClasses().ShouldBe(expected.AsEnumerable(), ignoreOrder: true);
    }
}
