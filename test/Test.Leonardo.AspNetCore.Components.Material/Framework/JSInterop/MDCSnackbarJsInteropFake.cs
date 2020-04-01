using Microsoft.AspNetCore.Components;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop
{
    internal class MDCSnackbarJsInteropFake : MDCComponentJsInterop<MDCSnackbar>
    {
        public Task AttachTo(object[] args)
        {
            args.Length.ShouldBe(1);
            var elementRef = args[0].ShouldBeOfType<ElementReference>();
            elementRef.Id.ShouldNotBeNullOrWhiteSpace();

            return Task.CompletedTask;
        }

        public override IDictionary<string, Func<object[], Task>> GetFunctionsDefinitions() => new Dictionary<string, Func<object[], Task>>
        {
            { "MDCSnackbarComponent.attachTo", AttachTo }
        };
    }
}
