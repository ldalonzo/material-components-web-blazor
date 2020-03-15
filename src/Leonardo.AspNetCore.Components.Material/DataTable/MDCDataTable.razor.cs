using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Text;

namespace Leonardo.AspNetCore.Components.Material.DataTable
{
    /// <summary>
    /// Data tables display information in a way that's easy to scan, so that users can look for patterns and insights.
    /// </summary>
    public abstract class MDCDataTable : MaterialComponent
    {
        [Parameter] public string Label { get; set; }

        protected override string BuildClassString()
        {
            var sb = new StringBuilder();

            sb.Append("mdc-data-table");

            if (!string.IsNullOrWhiteSpace(Class))
            {
                sb.Append($" {Class}");
            }

            return sb.ToString();
        }
    }

    public partial class MDCDataTable<TItem> : MDCDataTable
    {
        [Parameter] public IReadOnlyList<TItem> Items { get; set; }
    }
}
