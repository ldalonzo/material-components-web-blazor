using AutoFixture.Xunit2;
using Leonardo.AspNetCore.Components.Material;
using Microsoft.AspNetCore.Components.Testing;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public abstract class MaterialComponentUnitTest<T>
        where T : MaterialComponent
    {
        public MaterialComponentUnitTest(IServiceCollection services = null)
        {
            host = new TestHost(services);
        }

        protected readonly TestHost host;

        protected RenderedComponent<T> AddComponent(params (string, object)[] parameters)
        {
            var component = host.AddComponent<T>(parameters);

            return component;
        }

        [Fact]
        public void TestComponentLifecycle()
        {
            var component = AddComponent();
            component.GetMarkup().ShouldNotBeEmpty();
        }

        [Theory, AutoData]
        public void TestRenderCustomCssClass(string customCssClass)
        {
            var component = AddComponent((nameof(MaterialComponent.Class), customCssClass));
            component.GetMarkup().ShouldContain(customCssClass);
        }
    }
}
