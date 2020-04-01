using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes
{
    internal abstract class MDCComponentJsInterop<T> : IJSInteropComponent
        where T : MDCComponent
    {
        protected readonly IDictionary<string, T> componentsById = new Dictionary<string, T>();

        public abstract IDictionary<string, Func<object[], Task>> GetFunctionsDefinitions();

        internal T FindComponentById(string id)
            => componentsById.TryGetValue(id, out var component) ? component : (default);
    }
}
