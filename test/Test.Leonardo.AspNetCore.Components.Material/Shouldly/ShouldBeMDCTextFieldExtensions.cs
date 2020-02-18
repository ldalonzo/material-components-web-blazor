using HtmlAgilityPack;
using Leonardo.AspNetCore.Components.Material.TextField;
using Microsoft.AspNetCore.Components.Testing;
using Shouldly;

namespace Test.Leonardo.AspNetCore.Components.Material.Shouldly
{
    public static class ShouldBeMDCTextFieldExtensions
    {
        public static HtmlNode ShouldHaveMdcTextFieldNode(this RenderedComponent<MDCTextField> sut)
        {
            var rootNode = sut.Find("div");
            sut.ShouldNotBeNull();
            return rootNode;
        }

        public static HtmlNode ShouldHaveInputNode(this RenderedComponent<MDCTextField> sut)
        {
            var inputNode = sut.Find("input");
            inputNode.ShouldNotBeNull();
            return inputNode;
        }
    }
}

