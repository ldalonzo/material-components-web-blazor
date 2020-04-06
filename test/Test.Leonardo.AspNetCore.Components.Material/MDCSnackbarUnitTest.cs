using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Snackbar;
using Microsoft.JSInterop;
using Shouldly;
using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class MDCSnackbarUnitTest : MaterialComponentUnitTest<MDCSnackbar>
    {
        public MDCSnackbarUnitTest()
        {
            FakeComponents = new MDCSnackbarJsInteropFake();
            host.AddService<IJSRuntime>(new JSRuntimeFake(FakeComponents));
        }

        private readonly MDCSnackbarJsInteropFake FakeComponents;

        [Fact]
        public void HtmlStructure_MdcSnackbar()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div").ShouldHaveSingleItem();
            divElement.ShouldContainCssClasses("mdc-snackbar");
        }

        [Fact]
        public void HtmlStructure_MdcSnackbar_Surface()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div/div[1]").ShouldHaveSingleItem();
            divElement.ShouldContainCssClasses("mdc-snackbar__surface");
        }

        [Fact]
        public void HtmlStructure_MdcSnackbar_Label_CssClass()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div/div[1]/div").ShouldHaveSingleItem();
            divElement.ShouldContainCssClasses("mdc-snackbar__label");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcSnackbar_Label_Content(string label)
        {
            var sut = AddComponent(("Text", label));

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div/div[1]/div").ShouldHaveSingleItem();
            divElement.InnerText.Trim().ShouldBe(label);
        }

        [Fact]
        public void HtmlStructure_MdcSnackbar_Label_Role()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div/div[1]/div").ShouldHaveSingleItem();
            var roleAttribute = divElement.Attributes["role"];
            roleAttribute.ShouldNotBeNull();
            roleAttribute.Value.ShouldBe("status");
        }

        [Fact]
        public void HtmlStructure_MdcSnackbar_Label_Aria()
        {
            var sut = AddComponent();

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div/div[1]/div").ShouldHaveSingleItem();
            var ariaAttribute = divElement.Attributes["aria-live"];
            ariaAttribute.ShouldNotBeNull();
            ariaAttribute.Value.ShouldBe("polite");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcSnackbar_Actions(string buttonLabel)
        {
            var sut = AddComponent(("ButtonLabel", buttonLabel));

            var rootNode = sut.GetDocumentNode();
            var divElement = rootNode.SelectNodes("/div/div/div[2]").ShouldHaveSingleItem();
            divElement.ShouldContainCssClasses("mdc-snackbar__actions");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcSnackbar_Actions_Button(string buttonLabel)
        {
            var sut = AddComponent(("ButtonLabel", buttonLabel));

            var rootNode = sut.GetDocumentNode();
            var buttonElement = rootNode.SelectNodes("/div/div/div[2]/button").ShouldHaveSingleItem();
            buttonElement.ShouldContainCssClasses("mdc-button", "mdc-snackbar__action");
            var typeAttribute = buttonElement.Attributes["type"];
            typeAttribute.ShouldNotBeNull();
            typeAttribute.Value.ShouldBe("button");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcSnackbar_Actions_Button_Ripple(string buttonLabel)
        {
            var sut = AddComponent(("ButtonLabel", buttonLabel));

            var rootNode = sut.GetDocumentNode();
            var rippleElement = rootNode.SelectNodes("/div/div/div[2]/button/div").ShouldHaveSingleItem();
            rippleElement.ShouldContainCssClasses("mdc-button__ripple");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcSnackbar_Actions_Button_Label(string buttonLabel)
        {
            var sut = AddComponent(("ButtonLabel", buttonLabel));

            var rootNode = sut.GetDocumentNode();
            var labelElement = rootNode.SelectNodes("/div/div/div[2]/button/span").ShouldHaveSingleItem();
            labelElement.ShouldContainCssClasses("mdc-button__label");
            labelElement.InnerText.Trim().ShouldBe(buttonLabel);
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcSnackbar_Actions_DismissIcon(string buttonLabel)
        {
            var sut = AddComponent(
                ("ButtonLabel", buttonLabel),
                ("HasDismissIcon", true));

            var rootNode = sut.GetDocumentNode();
            var dismissButton = rootNode.SelectNodes("/div/div/div[2]/button[2]").ShouldHaveSingleItem();
            dismissButton.ShouldContainCssClasses("mdc-icon-button", "mdc-snackbar__dismiss", "material-icons");
            dismissButton.InnerText.Trim().ShouldBe("close");
        }

        [Theory]
        [AutoData]
        public void HtmlStructure_MdcSnackbar_Actions_DismissIcon_Title(string buttonLabel)
        {
            var sut = AddComponent(
                ("ButtonLabel", buttonLabel),
                ("HasDismissIcon", true));

            var rootNode = sut.GetDocumentNode();
            var dismissButton = rootNode.SelectNodes("/div/div/div[2]/button[2]").ShouldHaveSingleItem();
            var titleAttribute = dismissButton.Attributes["title"];
            titleAttribute.ShouldNotBeNull();
            titleAttribute.Value.ShouldBe("Dismiss");
        }

        [Fact]
        public async Task Open()
        {
            var sut = AddComponent();

            await sut.Instance.Open();

            var fake = FakeComponents.FindComponentById(sut.Instance.ElementReferenceId);
            fake.ShouldNotBeNull();
            fake.IsOpen.ShouldBeTrue();
        }

        [Theory]
        [AutoData]
        public void LabelTextHasTheMostUpToDateValue(string text)
        {
            var sut = AddComponent(("Text", text));

            var fake = FakeComponents.FindComponentById(sut.Instance.ElementReferenceId);
            fake.ShouldNotBeNull();
            fake.LabelText.ShouldBe(text);
        }
    }
}
