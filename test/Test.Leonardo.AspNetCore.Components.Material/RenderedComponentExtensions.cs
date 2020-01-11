using HtmlAgilityPack;
using Leonardo.AspNetCore.Components.Material.Select;
using Microsoft.AspNetCore.Components.Testing;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public static class RenderedComponentExtensions
    {
        public static HtmlNodeCollection GetListItems<T>(this RenderedComponent<MDCSelect<T>> source)
            => source.Find("div").SelectNodes("div/ul/li");
    }
}
