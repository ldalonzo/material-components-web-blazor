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
        protected override string ComponentIdentifier => "MDCSnackbarComponent";

        public Task Open(object[] args)
        {
            args.Length.ShouldBe(1);
            var elementRef = args[0].ShouldBeOfType<ElementReference>();
            elementRef.Id.ShouldNotBeNullOrWhiteSpace();

            var component = FindComponentById(elementRef.Id);
            component.Open();

            return Task.CompletedTask;
        }

        public Task SetLabelText(object[] args)
        {
            args.Length.ShouldBe(2);
            var elementRef = args[0].ShouldBeOfType<ElementReference>();
            elementRef.Id.ShouldNotBeNullOrWhiteSpace();

            var component = FindComponentById(elementRef.Id);
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
