using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material.Select;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Moq;
using Shouldly;
using System.Reflection;
using System.Threading.Tasks;
using Test.Blazor.Material.Components;
using Test.Leonardo.AspNetCore.Components.Material.Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public abstract class MDCSelectUnitTest<T> : MaterialComponentUnitTest<MDCSelect<T>>
    {
        public MDCSelectUnitTest()
        {
            jsMock = new Mock<IJSRuntime>(MockBehavior.Strict);

            jsMock
                .Setup(r => r.InvokeAsync<object>(
                    It.Is<string>(identifier => identifier == "MDCSelectComponent.attachTo"),
                    It.Is<object[]>(args => MatchAttachToArguments(args))))
                .Returns(new ValueTask<object>())
                .Verifiable();

            jsMock
                .Setup(r => r.InvokeAsync<object>(
                    It.Is<string>(identifier => identifier == "MDCSelectComponent.setSelectedIndex"),
                    It.Is<object[]>(args => true)))
                .Returns(new ValueTask<object>());

            host.AddService(jsMock.Object);
        }

        protected readonly Mock<IJSRuntime> jsMock;

        [Fact]
        public void Style_HasMandatoryCssClasses()
        {
            var select = AddComponent();
            select.Find("div").ShouldContainCssClasses("mdc-select");
        }

        [Theory]
        [AutoData]
        public void Label_IsRendered(string label)
        {
            var select = AddComponent(("Label", label));

            // ASSERT the label is present in the markup.
            select.GetMarkup().ShouldContain(label);

            // ASSERT the label is in the right place.
            var floatingLabelNode = select.FindFloatingLabelNode();
            floatingLabelNode.ShouldNotBeNull();
            floatingLabelNode.ChildNodes.ShouldNotBeEmpty();
            floatingLabelNode.ChildNodes.ShouldHaveSingleItem().InnerText.ShouldBe(label);
        }

        [Fact]
        public void Dropdown_IsRendered()
        {
            var select = AddComponent();

            var selectListNode = select.Find("div").SelectSingleNode("div/ul");
            selectListNode.ShouldContainCssClasses("mdc-list");
        }

        [Fact]
        public void JavaScriptInstantiation()
        {
            var textField = AddComponent();

            jsMock.Verify(
                r => r.InvokeAsync<object>("MDCSelectComponent.attachTo", It.IsAny<object[]>()),
                Times.Once);
        }

        protected static Task InvokeMethodAsync<TComponent>(DotNetObjectReference<TComponent> target, string methodName, params object[] args)
            where TComponent : class
        {
            var targetMethod = typeof(TComponent).GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
            targetMethod.ShouldNotBeNull();

            return (Task)targetMethod.Invoke(target.Value, args);
        }

        private static bool MatchAttachToArguments(object[] args)
        {
            if (args.Length != 2)
            {
                return false;
            }

            if (args[0].GetType() != typeof(ElementReference))
            {
                return false;
            }

            var elementReference = (ElementReference)args[0];
            if (string.IsNullOrEmpty(elementReference.Id))
            {
                return false;
            }

            var dotNetObjectRef = args[1] as DotNetObjectReference<MDCSelect>;
            if (dotNetObjectRef == null)
            {
                return false;
            }

            return true;
        }
    }
}
