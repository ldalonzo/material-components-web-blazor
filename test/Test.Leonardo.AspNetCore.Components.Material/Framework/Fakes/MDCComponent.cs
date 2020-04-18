using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes
{
    internal abstract class MDCComponent
    {
        private readonly IDictionary<string, List<Func<Task>>> handlersByEventName = new Dictionary<string, List<Func<Task>>>();

        public void Listen(string eventName, Func<Task> handler)
        {
            if (!handlersByEventName.TryGetValue(eventName, out var handlersForEvent))
            {
                handlersForEvent = new List<Func<Task>>();
                handlersByEventName.Add(eventName, handlersForEvent);
            }

            handlersForEvent.Add(handler);
        }

        public Task Emit(string eventName) => handlersByEventName.TryGetValue(eventName, out var handlersForEvent)
            ? Task.WhenAll(handlersForEvent.Select(handle => handle()))
            : Task.CompletedTask;
    }

    internal class MDCComponent<T> : MDCComponent
        where T : MDCFoundation
    {
    }
}
