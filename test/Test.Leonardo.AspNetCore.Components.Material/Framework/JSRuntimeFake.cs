using Microsoft.JSInterop;
using System;
using System.Threading;
using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Framework;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class JSRuntimeFake : IJSRuntime
    {
        public JSRuntimeFake(MDCTopAppBarJsInteropFake jsComponent)
        {
            this.jsComponent = jsComponent;
        }

        private readonly MDCTopAppBarJsInteropFake jsComponent;

        public async ValueTask<TValue> InvokeAsync<TValue>(string identifier, object[] args)
        {
            switch (identifier)
            {
                case MDCTopAppBarJsInteropFake.attachTo:
                    await jsComponent.AttachTo(args);
                    return default;

                case MDCTopAppBarJsInteropFake.listenToNav:
                    await jsComponent.ListenToNav(args);
                    return default;

                default:
                    throw new InvalidOperationException();
            }
        }

        public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object[] args) => throw new NotImplementedException();
    }
}
