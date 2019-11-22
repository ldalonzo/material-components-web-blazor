using Microsoft.AspNetCore.Components;

namespace LD.AspNetCore.Components.Testing
{
    public class TestHost
    {
        public RenderedComponent<TComponent> AddComponent<TComponent>() where TComponent : IComponent
        {
            var result = new RenderedComponent<TComponent>();
            result.SetParametersAndRender(ParameterView.Empty);
            return result;
        }
    }
}
