using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop
{
    internal class MDCSnackbarJsInteropFake : MDCComponentJsInterop<MDCSnackbarFake>
    {
        protected override string ComponentIdentifier => "MDCSnackbarComponent";

        public Task Open(object[] args)
        {
            args.Length.ShouldBe(1);
            var id = args[0].ShouldBeOfType<string>();
            var component = FindComponentById(id);
            component.Open();

            return Task.CompletedTask;
        }

        public Task SetLabelText(object[] args)
        {
            args.Length.ShouldBe(2);
            var id = args[0].ShouldBeOfType<string>();
            var component = FindComponentById(id);
            component.ShouldNotBeNull();
            component.LabelText = args[1].ShouldBeOfType<string>();

            return Task.CompletedTask;
        }

        protected override IEnumerable<(string, Func<object[], Task>)> EnumerateFunctionsDefinitions()
        {
            yield return ("open", Open);
            yield return ("setLabelText", SetLabelText);
        }
    }
}
