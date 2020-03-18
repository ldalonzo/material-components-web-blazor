using Microsoft.AspNetCore.Components;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Components;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework
{
    public class MDCListJsIteropFake : IJSInteropComponent
    {
        private readonly IDictionary<string, MDCListFoundationFake> componentsById = new Dictionary<string, MDCListFoundationFake>();

        public Task AttachTo(object[] args)
        {
            args.Length.ShouldBe(2);
            var elementRef = args[0].ShouldBeOfType<ElementReference>();
            elementRef.Id.ShouldNotBeNullOrWhiteSpace();

            var wrapFocus = args[1].ShouldBeOfType<bool>();

            componentsById[elementRef.Id] = new MDCListFoundationFake { WrapFocus = wrapFocus };

            return Task.CompletedTask;
        }

        public MDCListFoundationFake FindComponentById(string id)
        {
            if (componentsById.TryGetValue(id, out var component))
            {
                return component;
            }

            return default;
        }

        public IDictionary<string, Func<object[], Task>> GetFunctionsDefinitions() => new Dictionary<string, Func<object[], Task>>
            {
                { "MDCListComponent.attachTo", AttachTo }
            };
    }
}
