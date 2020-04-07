using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Testing;
using Shouldly;
using System;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public abstract class MaterialComponentUnitTest<TComponent>
        where TComponent : IComponent
    {
        public MaterialComponentUnitTest(Action<TestHost> initialize = null)
        {
            host = new TestHost();
            initialize?.Invoke(host);
        }

        protected readonly TestHost host;

        protected RenderedComponent<TComponent> AddComponent(params (string, object)[] parameters)
            => host.AddComponent<TComponent>(parameters);

        [Fact]
        public void TestComponentLifecycle()
        {
            var component = AddComponent();
            component.GetMarkup().ShouldNotBeEmpty();
        }

        [Theory, AutoData]
        public void TestRenderCustomCssClass(string customCssClass)
        {
            var sut = AddComponent(("class", customCssClass));

            var rootNode = sut.GetDocumentNode().FirstChild;
            var classAttribute = rootNode.Attributes["class"];
            classAttribute.ShouldNotBeNull();

            var cssClasses = classAttribute.Value.Split();
            cssClasses.ShouldContain(customCssClass);
        }
    }
}
