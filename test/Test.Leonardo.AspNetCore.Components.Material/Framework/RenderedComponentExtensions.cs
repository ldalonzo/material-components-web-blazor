using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Testing;
using System.Linq;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public static class RenderedComponentExtensions
    {
        public static RenderedComponent<TComponent> AddComponent<TComponent>(this TestHost host, params (string, object)[] parameters) where TComponent : IComponent
            => host.AddComponent<TComponent>(parameters.ToDictionary(kv => kv.Item1, kv => kv.Item2));
    }
}
