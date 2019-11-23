using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace LD.AspNetCore.Components.Testing
{
    public class TestHost
    {
        public RenderedComponent<TComponent> AddComponent<TComponent>(IDictionary<string, object> parameters = null)
            where TComponent : IComponent
        {
            var result = new RenderedComponent<TComponent>();

            result.SetParametersAndRender(parameters != null
                ? ParameterView.FromDictionary(parameters)
                : ParameterView.Empty);

            return result;
        }
    }
}
