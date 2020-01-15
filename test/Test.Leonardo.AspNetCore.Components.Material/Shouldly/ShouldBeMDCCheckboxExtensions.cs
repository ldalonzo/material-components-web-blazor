using HtmlAgilityPack;
using Leonardo.AspNetCore.Components.Material.Checkbox;
using Microsoft.AspNetCore.Components.Testing;
using Shouldly;

namespace Test.Leonardo.AspNetCore.Components.Material.Shouldly
{
    public static class ShouldBeMDCCheckboxExtensions
    {
        public static HtmlNode ShouldHaveLabelNode(this RenderedComponent<MDCCheckbox> sut)
        {
            var labelNode = sut.Find("div").SelectSingleNode("label");
            labelNode.ShouldNotBeNull();

            return labelNode;
        }

        public static HtmlNode ShouldHaveInputNode(this RenderedComponent<MDCCheckbox> sut)
        {
            var inputNode = sut.Find("div").SelectSingleNode("div/input");
            inputNode.ShouldNotBeNull();

            return inputNode;
        }
    }
}

