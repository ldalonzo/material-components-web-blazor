using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop
{
    internal class MDCDrawerJsInteropFake : MDCComponentJsInterop<MDCDrawerFake>
    {
        protected override string ComponentIdentifier => "MDCDrawerComponent";

        public Task ToggleOpen(object[] args)
        {
            args.Length.ShouldBe(1);

            var foundation = FindComponentById(args[0]);
            foundation.Open = !foundation.Open;

            return Task.CompletedTask;
        }

        protected override IEnumerable<(string, Func<object[], Task>)> EnumerateFunctionsDefinitions()
        {
            yield return ("toggleOpen", ToggleOpen);
        }
    }
}
