using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes
{
    internal abstract class MDCComponentJsInterop<T> : IJSInteropComponent
        where T : MDCComponentFake, new()
    {
        protected abstract string ComponentIdentifier { get; }

        protected readonly IDictionary<string, T> componentsById = new Dictionary<string, T>();

        public IDictionary<string, Func<object[], Task>> GetFunctionsDefinitions()
        {
            var definitions = new Dictionary<string, Func<object[], Task>>();

            foreach (var (name, handler) in EnumerateFunctionsDefinitions().Append(("attachTo", AttachTo)))
            {
                definitions.Add($"{ComponentIdentifier}.{name}", handler);
            }

            return definitions;
        }

        protected virtual IEnumerable<(string, Func<object[], Task>)> EnumerateFunctionsDefinitions()
        {
            yield break;
        }

        internal T FindComponentById(string id)
        {
            componentsById.ShouldContainKey(id);
            return componentsById[id];
        }

        internal T FindComponentById(object arg)
            => FindComponentById(arg.ShouldBeOfType<string>());

        public virtual Task AttachTo(object[] args)
        {
            args.Length.ShouldBe(2);
            args[0].ShouldBeOfType<ElementReference>();
            var id = args[1].ShouldBeOfType<string>();

            componentsById.Add(id, new T());

            return Task.CompletedTask;
        }

        protected static Task InvokeMethodAsync<TComponent>(DotNetObjectReference<TComponent> dotnetHelper, string methodName, params object[] args)
            where TComponent : class
        {
            var methodInfo = dotnetHelper.Value.GetType().GetMethod(methodName);
            if (methodInfo == null)
            {
                throw new ArgumentException();
            }

            return (Task)methodInfo.Invoke(dotnetHelper.Value, args);
        }
    }
}
