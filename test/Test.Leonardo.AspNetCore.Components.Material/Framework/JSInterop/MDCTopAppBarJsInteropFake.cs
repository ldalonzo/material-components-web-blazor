using Leonardo.AspNetCore.Components.Material.TopAppBar;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop
{
    internal class MDCTopAppBarJsInteropFake : MDCComponentJsInterop<MDCTopAppBarFake>
    {
        protected override string ComponentIdentifier => "MDCTopAppBarComponent";

        public Task ListenToNav(object[] args)
        {
            args.Length.ShouldBe(2);

            var id = args[0].ShouldBeOfType<string>();
            componentsById.ShouldContainKey(id);
            var mdcComponent = componentsById[id];

            mdcComponent.Listen("MDCTopAppBar:nav", _ => InvokeMethodAsync(
                args[1].ShouldBeOfType<DotNetObjectReference<MDCTopAppBar>>(), "OnMDCTopAppBarNav"));

            return Task.CompletedTask;
        }

        public override Task AttachTo(object[] args) 
            => AttachToWithExplicitId(args);

        protected override IEnumerable<(string, Func<object[], Task>)> EnumerateFunctionsDefinitions()
        {
            yield return ("listenToNav", ListenToNav);
        }
    }
}
