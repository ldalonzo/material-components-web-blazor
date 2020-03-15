using Microsoft.AspNetCore.Components;
using System;

namespace Leonardo.AspNetCore.Components.Material
{
    public abstract class MaterialComponent : ComponentBase
    {
        [Parameter] public string Class { get; set; }

        protected string ClassString { get; private set; }

        protected string Id { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (string.IsNullOrWhiteSpace(Id))
            {
                Id = $"{GetType().Name}-{Guid.NewGuid().ToString().Substring(0, 4)}".ToLower();
            }

            ClassString = BuildClassString();
        }

        protected abstract string BuildClassString();
    }
}
