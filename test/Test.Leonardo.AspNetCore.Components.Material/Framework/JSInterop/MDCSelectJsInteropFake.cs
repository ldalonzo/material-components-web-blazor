using Leonardo.AspNetCore.Components.Material.Select;
using Microsoft.JSInterop;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop
{
    public class MDCSelectJsInteropFake : MDCComponentJsInterop<MDCSelectFake>
    {
        protected override string ComponentIdentifier => "MDCSelectComponent";

        public virtual Task ListenToChange(object[] args)
        {
            args.Length.ShouldBe(2);

            var component = FindComponentById(args[0]);

            component.Listen("MDCSelect:change", e => InvokeMethodAsync(
               args[1].ShouldBeOfType<DotNetObjectReference<MDCSelect>>(), "OnChange", e));

            return Task.CompletedTask;
        }

        public virtual Task SetSelectedIndex(object[] args)
        {
            args.Length.ShouldBe(2);

            var component = FindComponentById(args[0]);
            component.SelectedIndex = args[1].ShouldBeOfType<int>();

            return Task.CompletedTask;
        }

        protected override IEnumerable<(string, Func<object[], Task>)> EnumerateFunctionsDefinitions()
        {
            yield return ("listenToChange", ListenToChange);
            yield return ("setSelectedIndex", SetSelectedIndex);
        }
    }
}
