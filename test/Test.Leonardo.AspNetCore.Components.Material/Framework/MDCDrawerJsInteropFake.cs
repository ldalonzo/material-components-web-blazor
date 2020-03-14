using Microsoft.AspNetCore.Components;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Components;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework
{
    public class MDCDrawerJsInteropFake : IJSInteropComponent
    {
        private readonly IDictionary<string, MDCDismissibleDrawerFoundationFake> componentsById = new Dictionary<string, MDCDismissibleDrawerFoundationFake>();

        public Task AttachTo(object[] args)
        {
            args.Length.ShouldBe(1);
            var elementRef = args[0].ShouldBeOfType<ElementReference>();
            elementRef.Id.ShouldNotBeNullOrWhiteSpace();

            componentsById.Add(elementRef.Id, new MDCDismissibleDrawerFoundationFake());

            return Task.CompletedTask;
        }

        public Task ToggleOpen(object[] args)
        {
            args.Length.ShouldBe(1);
            var elementRef = args[0].ShouldBeOfType<ElementReference>();
            elementRef.Id.ShouldNotBeNullOrWhiteSpace();

            componentsById.ShouldContainKey(elementRef.Id);
            var foundation = componentsById[elementRef.Id];
            foundation.Open = !foundation.Open;

            return Task.CompletedTask;
        }

        public MDCDismissibleDrawerFoundationFake FindComponentById(string id)
        {
            if (componentsById.TryGetValue(id, out var component))
            {
                return component;
            }

            return default;
        }

        public IDictionary<string, Func<object[], Task>> GetFunctionsDefinitions() => new Dictionary<string, Func<object[], Task>>
            {
                { "MDCDrawerComponent.attachTo", AttachTo },
                { "MDCDrawerComponent.toggleOpen", ToggleOpen }
            };
    }
}
