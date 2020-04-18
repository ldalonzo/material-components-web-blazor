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
    internal class MDCTopAppBarJsInteropFake : MDCComponentJsInterop<MDCTopAppBar>
    {
        public Task ListenToNav(object[] args)
        {
            args.Length.ShouldBe(2);

            var elementRef = args[0].ShouldBeOfType<ElementReference>();
            elementRef.Id.ShouldNotBeNullOrWhiteSpace();

            componentsById.ShouldContainKey(elementRef.Id);
            var mdcComponent = componentsById[elementRef.Id];

            mdcComponent.Listen("MDCTopAppBar:nav", () => InvokeMethodAsync(
                args[1].ShouldBeOfType<DotNetObjectReference<global::Leonardo.AspNetCore.Components.Material.TopAppBar.MDCTopAppBar>>(), "OnMDCTopAppBarNav"));

            static Task InvokeMethodAsync<T>(DotNetObjectReference<T> dotnetHelper, string methodName) where T : class
            {
                var methodInfo = dotnetHelper.Value.GetType().GetMethod(methodName);
                if (methodInfo == null)
                {
                    throw new ArgumentException();
                }

                return (Task)methodInfo.Invoke(dotnetHelper.Value, null);
            }

            return Task.CompletedTask;
        }

        public override IDictionary<string, Func<object[], Task>> GetFunctionsDefinitions() => new Dictionary<string, Func<object[], Task>>
            {
                { "MDCTopAppBarComponent.attachTo", AttachTo },
                { "MDCTopAppBarComponent.listenToNav", ListenToNav }
            };
    }
}
