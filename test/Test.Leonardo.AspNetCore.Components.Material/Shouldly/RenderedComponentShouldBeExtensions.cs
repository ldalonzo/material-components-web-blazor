using HtmlAgilityPack;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace Test.Leonardo.AspNetCore.Components.Material.Shouldly
{
    public static class RenderedComponentShouldBeExtensions
    {
        public static IEnumerable<string> GetCssClasses(this HtmlNode target)
        { 
            var classAttribute = target.Attributes["class"];
            classAttribute.ShouldNotBeNull();

            return classAttribute.Value.Split();
        }

        public static void ShouldContainCssClasses(this HtmlNode target, params string[] expected)
        { 
            target.GetCssClasses().ShouldBe(expected.AsEnumerable(), ignoreOrder: true);
        }
    }
}
