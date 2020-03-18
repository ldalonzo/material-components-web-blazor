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
            var rootNode = sut.Find("label");
            sut.ShouldNotBeNull();
            return rootNode;
        }

        public static HtmlNode ShouldHaveInputNode(this RenderedComponent<MDCTextField> source)
        {
            var root = source.GetDocumentNode();

            var inputNode = root.SelectNodes("//label/input").ShouldHaveSingleItem();
            inputNode.ShouldNotBeNull();

            return inputNode;
        }

        public static HtmlNode ShouldHaveLabelNode(this RenderedComponent<MDCTextField> source, MDCTextFieldStyle variant)
        {
            var root = source.GetDocumentNode();

            HtmlNode labelNode = null;
            switch (variant)
            {
                case MDCTextFieldStyle.Filled:
                    labelNode = root.SelectNodes("//label/span[2]").ShouldHaveSingleItem();
                    break;

                case MDCTextFieldStyle.Outlined:
                    labelNode = root.SelectNodes("//label/span/span/span").ShouldHaveSingleItem();
                    break;
            }

            labelNode.ShouldNotBeNull();
            return labelNode;
        }
    }
}

