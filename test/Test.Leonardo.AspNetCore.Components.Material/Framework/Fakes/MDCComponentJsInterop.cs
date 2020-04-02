using Microsoft.AspNetCore.Components;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes
{
    internal abstract class MDCComponentJsInterop<T> : IJSInteropComponent
        where T : MDCComponent, new()
    {
        protected readonly IDictionary<string, T> componentsById = new Dictionary<string, T>();

        public abstract IDictionary<string, Func<object[], Task>> GetFunctionsDefinitions();

        internal T FindComponentById(string id)
            => componentsById.TryGetValue(id, out var component) ? component : (default);

        public Task AttachTo(object[] args)
        {
            args.Length.ShouldBe(1);
            var elementRef = args[0].ShouldBeOfType<ElementReference>();
            elementRef.Id.ShouldNotBeNullOrWhiteSpace();

            componentsById.Add(elementRef.Id, new T());

            return Task.CompletedTask;
        }
    }
}
