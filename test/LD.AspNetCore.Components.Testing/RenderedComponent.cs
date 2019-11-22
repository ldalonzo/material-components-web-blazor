using Microsoft.AspNetCore.Components;

namespace LD.AspNetCore.Components.Testing
{
    public class RenderedComponent<TComponent> where TComponent : IComponent
    {
        public RenderedComponent()
        {
            Item = System.Activator.CreateInstance<TComponent>();
        }

        public TComponent Item { get; }

        internal void SetParametersAndRender(ParameterView parameters)
        {
            Item.SetParametersAsync(parameters);
        }
    }
}
