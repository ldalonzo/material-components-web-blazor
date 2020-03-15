using Microsoft.AspNetCore.Components;
using System;
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

        [Parameter] public RenderFragment ChildContent { get; set; }

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

        private readonly IList<MDCDataTableColumn> columns = new List<MDCDataTableColumn>();
        protected IEnumerable<MDCDataTableColumn> Columns => columns;

        internal void AddColumn(MDCDataTableColumn column)
        {
            columns.Add(column);
            StateHasChanged();
        }
    }

    public partial class MDCDataTable<TItem> : MDCDataTable
    {
        [Parameter] public IReadOnlyList<TItem> DataSource { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (DataSource == null)
            {
                DataSource = new TItem[0];
            }
        }
    }
}
