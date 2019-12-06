using Microsoft.AspNetCore.Components;
using System;
using System.Text;

namespace Leonardo.AspNetCore.Components.Material.TextField
{
    /// <summary>
    /// Text fields allow users to input, edit, and select text.
    /// </summary>
    /// <seealso href="https://material.io/develop/web/components/input-controls/text-field/"/>
    public partial class MDCTextField : MaterialComponent
    {
        [Parameter] public string Label { get; set; }

        protected string Id { get; set; } = $"text-field-{Guid.NewGuid().ToString().Substring(0, 3).ToLower()}";

        protected override string BuildClassString()
        {
            var sb = new StringBuilder();

            sb.Append("mdc-text-field");

            if (!string.IsNullOrWhiteSpace(Class))
            {
                sb.Append($" {Class}");
            }

            return sb.ToString();
        }
    }
}
