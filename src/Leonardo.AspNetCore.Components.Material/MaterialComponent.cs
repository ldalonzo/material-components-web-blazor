using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Leonardo.AspNetCore.Components.Material
{
    public abstract class MaterialComponent : ComponentBase
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> InputAttributes { get; set; }

        protected string ClassString { get; private set; }

        protected string Id { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (string.IsNullOrWhiteSpace(Id))
            {
                Id = $"{GetType().Name}-{Guid.NewGuid().ToString().Substring(0, 4)}".ToLower();
            }

            ClassString = BuildClassString(new StringBuilder()).ToString();
        }

        protected virtual StringBuilder BuildClassString(StringBuilder sb)
        {
            if (InputAttributes != null && InputAttributes.TryGetValue("class", out var customClass))
            {
                string customClassString = customClass as string;
                if (!string.IsNullOrWhiteSpace(customClassString))
                {
                    sb.Append($" {customClassString}");
                }
            }

            return sb;
        }

    }
}
