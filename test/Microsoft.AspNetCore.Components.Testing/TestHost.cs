using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Linq;

namespace Microsoft.AspNetCore.Components.Testing
{
    public class TestHost
    {
        private readonly IServiceCollection _serviceCollection;
        private readonly Lazy<TestRenderer> _renderer;

        public TestHost(IServiceCollection serviceCollection = null)
        {
            _serviceCollection = serviceCollection ?? new ServiceCollection();

            _renderer = new Lazy<TestRenderer>(() =>
            {
                var serviceProvider = _serviceCollection.BuildServiceProvider();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>() ?? new NullLoggerFactory();
                return new TestRenderer(serviceProvider, loggerFactory);
            });
        }

        public void AddService<T>(T implementation)
            => AddService<T, T>(implementation);

        public void AddService<TContract, TImplementation>(TImplementation implementation) where TImplementation: TContract
        {
            if (_renderer.IsValueCreated)
            {
                throw new InvalidOperationException("Cannot configure services after the host has started operation");
            }

            _serviceCollection.AddSingleton(typeof(TContract), implementation);
        }

        public void WaitForNextRender(Action trigger)
        {
            var task = Renderer.NextRender;
            trigger();
            task.Wait(millisecondsTimeout: 1000);

            if (!task.IsCompleted)
            {
                throw new TimeoutException("No render occurred within the timeout period.");
            }
        }

        public RenderedComponent<TComponent> AddComponent<TComponent>(params (string, object)[] parameters) where TComponent: IComponent
        {
            var result = new RenderedComponent<TComponent>(Renderer);

            result.SetParametersAndRender(parameters.Any()
                ? ParameterView.FromDictionary(parameters.ToDictionary(kv => kv.Item1, kv => kv.Item2))
                : ParameterView.Empty);

            return result;
        }

        private TestRenderer Renderer => _renderer.Value;
    }
}
