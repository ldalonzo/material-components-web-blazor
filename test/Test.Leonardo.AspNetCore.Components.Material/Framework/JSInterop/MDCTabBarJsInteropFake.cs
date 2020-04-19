using Leonardo.AspNetCore.Components.Material.TabBar;
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
    internal class MDCTabBarJsInteropFake : MDCComponentJsInterop<MDCTabBarFake>
    {
        protected override string ComponentIdentifier => "MDCTabBarComponent";

        public Task ListenToActivated(object[] args)
        {
            args.Length.ShouldBe(2);

            var id = args[0].ShouldBeOfType<string>();
            componentsById.ShouldContainKey(id);
            var component = componentsById[id];

            component.Listen("MDCTabBar:activated", e => InvokeMethodAsync(
               args[1].ShouldBeOfType<DotNetObjectReference<MDCTabBar>>(), "OnTabActivated", e));

            return Task.CompletedTask;
        }

        protected override IEnumerable<(string, Func<object[], Task>)> EnumerateFunctionsDefinitions()
        {
            yield return ("listenToActivated", ListenToActivated);
        }

        public override Task AttachTo(object[] args)
            => AttachToWithExplicitId(args);
    }
}
