using Leonardo.AspNetCore.Components.Material;
using Microsoft.AspNetCore.Components.Testing;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace Test.Blazor.Material.Components
{
    public static class RenderedComponentShouldBeExtensions
    {
        public static IEnumerable<string> GetCssClassesForElement<T>(this RenderedComponent<T> target, string targetElement)
            where T : MaterialComponent
        {
            var divElement = target.Find(targetElement);
            divElement.ShouldNotBeNull();

            return divElement.Attributes.Where(a => a.Name == "class").ShouldHaveSingleItem().Value.Split();
        }
    }
}
