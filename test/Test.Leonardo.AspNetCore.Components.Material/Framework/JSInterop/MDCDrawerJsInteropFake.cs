using Microsoft.AspNetCore.Components;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop
{
    internal class MDCDrawerJsInteropFake : MDCComponentJsInterop<MDCDrawer>
    {
        public Task AttachTo(object[] args)
        {
            args.Length.ShouldBe(1);
            var elementRef = args[0].ShouldBeOfType<ElementReference>();
            elementRef.Id.ShouldNotBeNullOrWhiteSpace();

            componentsById.Add(elementRef.Id, new MDCDrawer());

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

        public override IDictionary<string, Func<object[], Task>> GetFunctionsDefinitions() => new Dictionary<string, Func<object[], Task>>
            {
                { "MDCDrawerComponent.attachTo", AttachTo },
                { "MDCDrawerComponent.toggleOpen", ToggleOpen }
            };
    }
}
