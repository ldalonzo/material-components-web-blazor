using Microsoft.AspNetCore.Components;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop
{
    internal class MDCListJsIteropFake : MDCComponentJsInterop<MDCList>
    {
        public Task AttachToAndWrapFocus(object[] args)
        {
            args.Length.ShouldBe(2);
            var elementRef = args[0].ShouldBeOfType<ElementReference>();
            elementRef.Id.ShouldNotBeNullOrWhiteSpace();

            var wrapFocus = args[1].ShouldBeOfType<bool>();

            componentsById[elementRef.Id] = new MDCList { WrapFocus = wrapFocus };

            return Task.CompletedTask;
        }

        public override IDictionary<string, Func<object[], Task>> GetFunctionsDefinitions() => new Dictionary<string, Func<object[], Task>>
            {
                { "MDCListComponent.attachTo", AttachToAndWrapFocus }
            };
    }
}
