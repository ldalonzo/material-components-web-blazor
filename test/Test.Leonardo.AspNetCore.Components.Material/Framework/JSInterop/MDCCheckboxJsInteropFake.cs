using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop
{
    internal class MDCCheckboxJsInteropFake : MDCComponentJsInterop<MDCCheckboxFake>
    {
        protected override string ComponentIdentifier => "MDCCheckboxComponent";

        public virtual Task SetChecked(object[] args)
        {
            args.Length.ShouldBe(2);

            var component = FindComponentById(args[0]);
            component.Checked = args[1].ShouldBeOfType<bool>();

            return Task.CompletedTask;
        }

        protected override IEnumerable<(string, Func<object[], Task>)> EnumerateFunctionsDefinitions()
        {
            yield return ("setChecked", SetChecked);
        }
    }
}
