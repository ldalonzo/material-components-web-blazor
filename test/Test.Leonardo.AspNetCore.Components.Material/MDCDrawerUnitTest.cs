using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Drawer;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCDrawerUnitTest : MaterialComponentUnitTest<MDCDrawer>
    {
        public MDCDrawerUnitTest()
        {
            mdcListJsInterop = new MDCListJsIteropFake();
            mdcDrawerJsInterop = new MDCDrawerJsInteropFake();
            host.AddService<IJSRuntime, JSRuntimeFake>(new JSRuntimeFake(mdcDrawerJsInterop, mdcListJsInterop));
            host.AddService<NavigationManager>(new FakeNavigationManager());
        }

        private readonly MDCListJsIteropFake mdcListJsInterop;
        private readonly MDCDrawerJsInteropFake mdcDrawerJsInterop;

        [Fact]
        public void HtmlStructure_MdcDrawer()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var asideElement = rootNode.SelectNodes("//aside").ShouldHaveSingleItem();
            asideElement.ShouldContainCssClasses("mdc-drawer");
        }

        [Fact]
        public void HtmlStructure_MdcDrawer_Dismissable()
        {
            var sut = AddComponent(("Variant", MDCDrawerVariant.Dismissible));

            var rootNode = sut.GetDocumentNode();
            var asideElement = rootNode.SelectNodes("//aside").ShouldHaveSingleItem();
            asideElement.ShouldContainCssClasses("mdc-drawer", "mdc-drawer--dismissible", "mdc-drawer--open");
        }

        [Theory]
        [InlineData(MDCDrawerVariant.Default)]
        [InlineData(MDCDrawerVariant.Dismissible)]
        public void HtmlStructure_MdcDrawerContent(MDCDrawerVariant variant)
        {
            var sut = AddComponent(("Variant", variant));

            var rootNode = sut.GetDocumentNode();
            var asideElement = rootNode.SelectNodes("//aside/div").ShouldHaveSingleItem();
            asideElement.ShouldContainCssClasses("mdc-drawer__content");
        }

        [Theory]
        [InlineData(MDCDrawerVariant.Default)]
        [InlineData(MDCDrawerVariant.Dismissible)]
        public void HtmlStructure_Nav(MDCDrawerVariant variant)
        {
            var sut = AddComponent(("Variant", variant));

            var rootNode = sut.GetDocumentNode();
            var asideElement = rootNode.SelectNodes("//aside/div/nav").ShouldHaveSingleItem();
            asideElement.ShouldContainCssClasses("mdc-list");
        }

        [Theory]
        [InlineAutoData(MDCDrawerVariant.Default)]
        [InlineAutoData(MDCDrawerVariant.Dismissible)]
        public void HtmlStructure_SingleItem_CssClasses(MDCDrawerVariant variant, MDCDrawerNavLinkData item)
        {
            var sut = AddComponent(
                ("Variant", variant),
                ("DrawerContent", (RenderFragment)(b => BuildMDCDrawerNavLinkRenderFragment(b, item))));

            var rootNode = sut.GetDocumentNode();
            var itemNode = rootNode.SelectNodes("//aside/div/nav/a").ShouldHaveSingleItem();
            itemNode.ShouldContainCssClasses("mdc-list-item");
        }

        [Theory]
        [InlineAutoData(MDCDrawerVariant.Default)]
        [InlineAutoData(MDCDrawerVariant.Dismissible)]
        public void HtmlStructure_SingleItem_Href(MDCDrawerVariant variant, MDCDrawerNavLinkData item)
        {
            var sut = AddComponent(
                ("Variant", variant),
                ("DrawerContent", (RenderFragment)(b => BuildMDCDrawerNavLinkRenderFragment(b, item))));

            var rootNode = sut.GetDocumentNode();
            var itemNode = rootNode.SelectNodes("//aside/div/nav/a").ShouldHaveSingleItem();
            var hrefAttribute = itemNode.Attributes["href"];
            hrefAttribute.ShouldNotBeNull();
            hrefAttribute.Value.ShouldBe(item.Href);
        }

        [Theory]
        [InlineAutoData(MDCDrawerVariant.Default)]
        [InlineAutoData(MDCDrawerVariant.Dismissible)]
        public void HtmlStructure_SingleItem_Icon(MDCDrawerVariant variant, MDCDrawerNavLinkData item)
        {
            var sut = AddComponent(
                ("Variant", variant),
                ("DrawerContent", (RenderFragment)(b => BuildMDCDrawerNavLinkRenderFragment(b, item))));

            var rootNode = sut.GetDocumentNode();
            var itemNode = rootNode.SelectNodes("//aside/div/nav/a/i").ShouldHaveSingleItem();
            itemNode.ShouldContainCssClasses("material-icons", "mdc-list-item__graphic");
            itemNode.InnerText.ShouldBe(item.Icon);
        }

        [Theory]
        [InlineAutoData(MDCDrawerVariant.Default)]
        [InlineAutoData(MDCDrawerVariant.Dismissible)]
        public void HtmlStructure_SingleItem_Text(MDCDrawerVariant variant, MDCDrawerNavLinkData item)
        {
            var sut = AddComponent(
                ("Variant", variant),
                ("DrawerContent", (RenderFragment)(b => BuildMDCDrawerNavLinkRenderFragment(b, item))));

            var rootNode = sut.GetDocumentNode();
            var itemsNode = rootNode.SelectNodes("//aside/div/nav/a/span").ShouldHaveSingleItem();

            itemsNode.ShouldContainCssClasses("mdc-list-item__text");
            itemsNode.InnerText.ShouldBe(item.Text);
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MultipleItems(IEnumerable<MDCDrawerNavLinkData> items)
        {
            items.ShouldNotBeEmpty();

            var sut = AddComponent(
                ("DrawerContent", (RenderFragment)(b => BuildMDCDrawerNavLinkRenderFragment(b, items.ToArray()))));

            var rootNode = sut.GetDocumentNode();
            var itemsNode = rootNode.SelectNodes("//aside/div/nav/a");

            itemsNode.Count.ShouldBe(items.Count());
        }

        [Theory]
        [AutoData]
        public void PermanentlyVisibleDrawer_JavaScriptInstantiation_List(IEnumerable<MDCDrawerNavLinkData> items)
        {
            var sut = AddComponent(
                ("DrawerContent", (RenderFragment)(b => BuildMDCDrawerNavLinkRenderFragment(b, items.ToArray()))));

            var jsComponent = mdcListJsInterop.FindComponentById(sut.Instance.MDCListId);
            jsComponent.ShouldNotBeNull();
            jsComponent.WrapFocus.ShouldBeTrue();
        }

        [Theory]
        [AutoData]
        public void PermanentlyVisibleDrawer_JavaScriptInstantiation_List2(IEnumerable<MDCDrawerNavLinkData> items)
        {
            var sut = AddComponent(
                ("DrawerContent", (RenderFragment)(b => BuildMDCDrawerNavLinkRenderFragment(b, items.ToArray()))));

            var jsComponent = mdcListJsInterop.FindComponentById(sut.Instance.MDCListId);
            jsComponent.ShouldNotBeNull();
            jsComponent.WrapFocus.ShouldBeTrue();
        }

        [Theory]
        [InlineAutoData(MDCDrawerVariant.Default)]
        [InlineAutoData(MDCDrawerVariant.Dismissible)]
        public void HtmlStructure_Title(MDCDrawerVariant variant, string title)
        {
            var sut = AddComponent(
                ("Variant", variant),
                ("Title", title));

            var rootNode = sut.GetDocumentNode();
            var headerNode = rootNode.SelectNodes("//aside/div[1]").ShouldHaveSingleItem();
            headerNode.ShouldContainCssClasses("mdc-drawer__header");

            var titleNode = rootNode.SelectNodes("//aside/div[1]/h3").ShouldHaveSingleItem();
            titleNode.InnerText.ShouldBe(title);
        }

        [Theory]
        [InlineAutoData(MDCDrawerVariant.Default)]
        [InlineAutoData(MDCDrawerVariant.Dismissible)]
        public void HtmlStructure_Subtitle(MDCDrawerVariant variant, string subtitle)
        {
            var sut = AddComponent(
                ("Variant", variant),
                ("Subtitle", subtitle));

            var rootNode = sut.GetDocumentNode();
            var headerNode = rootNode.SelectNodes("//aside/div[1]").ShouldHaveSingleItem();
            headerNode.ShouldContainCssClasses("mdc-drawer__header");

            var subtitleNode = rootNode.SelectNodes("//aside/div[1]/h6").ShouldHaveSingleItem();
            subtitleNode.InnerText.ShouldBe(subtitle);
        }

        [Theory]
        [InlineAutoData(MDCDrawerVariant.Dismissible)]
        public async Task DismissableDrawerToggleOpen(MDCDrawerVariant variant)
        {
            var sut = AddComponent(("Variant", variant));

            var component = mdcDrawerJsInterop.FindComponentById(sut.Instance.MDCDrawerElementId);
            component.Open.ShouldBeFalse();

            await sut.Instance.ToggleOpen();
            component.Open.ShouldBeTrue();
        }

        private static void BuildMDCDrawerNavLinkRenderFragment(RenderTreeBuilder b, params MDCDrawerNavLinkData[] navLinks)
        {
            int c = 0;
            foreach (var item in navLinks)
            {
                b.OpenComponent<MDCDrawerNavLink>(c++);
                b.AddAttribute(c++, "Text", item.Text);
                b.AddAttribute(c++, "Icon", item.Icon);
                b.AddAttribute(c++, "Href", item.Href);
                b.AddAttribute(c++, "Match", item.Match);
                b.CloseComponent();
            }
        }

        public class MDCDrawerNavLinkData
        {
            public string Text { get; set; }
            public string Icon { get; set; }
            public string Href { get; set; }
            public NavLinkMatch Match { get; set; }
        }
    }
}
