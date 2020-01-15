using Microsoft.AspNetCore.Components;
using System.Text;

namespace Leonardo.AspNetCore.Components.Material.Checkbox
{
    /// <summary>
    /// Checkboxes allow the user to select one or more items from a set.
    /// </summary>
    /// <seealso href="https://github.com/material-components/material-components-web/tree/master/packages/mdc-checkbox"/>
    public partial class MDCCheckbox : MaterialComponent
    {
        [Parameter] public string Label { get; set; }

        protected override string BuildClassString()
        {
            var sb = new StringBuilder();

            sb.Append("mdc-checkbox");

            if (!string.IsNullOrWhiteSpace(Class))
            {
                sb.Append($" {Class}");
            }

            return sb.ToString();
        }
    }
}
