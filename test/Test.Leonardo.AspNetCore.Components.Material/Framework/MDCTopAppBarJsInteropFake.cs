using Leonardo.AspNetCore.Components.Material.TopAppBar;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework
{

    public class MDCTopAppBarJsInteropFake : IJSInteropComponent
    {
        private readonly IDictionary<string, MDCTopAppBarJsFake> componentsById = new Dictionary<string, MDCTopAppBarJsFake>();

        public Task AttachTo(object[] args)
        {
            args.Length.ShouldBe(1);
            var elementRef = args[0].ShouldBeOfType<ElementReference>();
            elementRef.Id.ShouldNotBeNullOrWhiteSpace();

            componentsById.Add(elementRef.Id, new MDCTopAppBarJsFake());

            return Task.CompletedTask;
        }

        public Task ListenToNav(object[] args)
        {
            args.Length.ShouldBe(2);

            var elementRef = args[0].ShouldBeOfType<ElementReference>();
            elementRef.Id.ShouldNotBeNullOrWhiteSpace();

            componentsById.ShouldContainKey(elementRef.Id);
            var mdcComponent = componentsById[elementRef.Id];

            mdcComponent.Listen("MDCTopAppBar:nav", () => InvokeMethodAsync(
                args[1].ShouldBeOfType<DotNetObjectReference<MDCTopAppBar>>(), "OnMDCTopAppBarNav"));

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

        public MDCTopAppBarJsFake FindComponentById(string id)
        {
            if (componentsById.TryGetValue(id, out var component))
            {
                return component;
            }

            return default;
        }

        public IDictionary<string, Func<object[], Task>> GetFunctionsDefinitions() => new Dictionary<string, Func<object[], Task>>
            {
                { "MDCTopAppBarComponent.attachTo", AttachTo },
                { "MDCTopAppBarComponent.listenToNav", ListenToNav }
            };
    }
}
