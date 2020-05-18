using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes
{
    public abstract class MDCComponentFake
    {
        private readonly IDictionary<string, List<Func<object, Task>>> handlersByEventName = new Dictionary<string, List<Func<object, Task>>>();

        public void Listen(string eventName, Func<object, Task> handler)
        {
            if (!handlersByEventName.TryGetValue(eventName, out var handlersForEvent))
            {
                handlersForEvent = new List<Func<object, Task>>();
                handlersByEventName.Add(eventName, handlersForEvent);
            }

            handlersForEvent.Add(handler);
        }

        protected Task Emit(string eventName, object arg = null) => handlersByEventName.TryGetValue(eventName, out var handlersForEvent)
            ? Task.WhenAll(handlersForEvent.Select(handle => handle(arg)))
            : Task.CompletedTask;
    }

    public class MDCComponentFake<T> : MDCComponentFake
        where T : MDCFoundation
    {
    }
}
