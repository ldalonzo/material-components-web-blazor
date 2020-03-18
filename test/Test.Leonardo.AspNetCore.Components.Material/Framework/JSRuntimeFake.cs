using Microsoft.JSInterop;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Test.Leonardo.AspNetCore.Components.Material
{

    public class JSRuntimeFake : IJSRuntime
    {
        private readonly IDictionary<string, Func<object[], Task>> functionsByIdentifiers = new Dictionary<string, Func<object[], Task>>();

        public JSRuntimeFake(params IJSInteropComponent[] jsComponents)
        {
            foreach(var jsComponent in jsComponents)
            {
                foreach (var (identifier, func) in jsComponent.GetFunctionsDefinitions())
                {
                    functionsByIdentifiers.Add(identifier, func);
                }
            }
        }

        public async ValueTask<TValue> InvokeAsync<TValue>(string identifier, object[] args)
        {
            functionsByIdentifiers.ShouldContainKey(identifier);
            await functionsByIdentifiers[identifier](args);

            return default;
        }

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object[] args) => throw new NotImplementedException();
    }
}
