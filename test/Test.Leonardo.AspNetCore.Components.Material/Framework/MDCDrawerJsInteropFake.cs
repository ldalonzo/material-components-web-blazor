using Microsoft.AspNetCore.Components;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework
{
    public class MDCDrawerJsInteropFake : IJSInteropComponent
    {
        public Task AttachTo(object[] args)
        {
            args.Length.ShouldBe(1);
            var elementRef = args[0].ShouldBeOfType<ElementReference>();
            elementRef.Id.ShouldNotBeNullOrWhiteSpace();

            return Task.CompletedTask;
        }

        public IDictionary<string, Func<object[], Task>> GetFunctionsDefinitions() => new Dictionary<string, Func<object[], Task>>
            {
                { "MDCDrawerComponent.attachTo", AttachTo }
            };
    }
}
