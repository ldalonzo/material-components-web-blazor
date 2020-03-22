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

        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter] public Density Density { get; set; }

        protected override string BuildClassString()
        {
            var sb = new StringBuilder();

            sb.Append("mdc-data-table");

            switch (Density)
            {
                case Density.Normal:
                    break;
                case Density.Dense2:
                    sb.Append(" mdc-data-table--density-2");
                    break;
                case Density.Dense4:
                    sb.Append(" mdc-data-table--density-4");
                    break;
            }

            if (!string.IsNullOrWhiteSpace(Class))
            {
                sb.Append($" {Class}");
            }

            return sb.ToString();
        }

        protected string GetItemDisplayText<TItem>(TItem item, string dataMember)
        {
            if (!string.IsNullOrWhiteSpace(dataMember))
            {
                var dataMemberProperty = typeof(TItem).GetProperty(dataMember);
                var value = dataMemberProperty.GetValue(item);
                if (value != null)
                {
                    if (value is string stringValue)
                    {
                        return stringValue;
                    }

                    return value.ToString();
                }
            }

            return string.Empty;
        }
    }

    public partial class MDCDataTable<TItem> : MDCDataTable
    {
        [Parameter] public IReadOnlyList<TItem> DataSource { get; set; }

        private readonly IList<MDCDataTableColumn<TItem>> columns = new List<MDCDataTableColumn<TItem>>();
        protected IEnumerable<MDCDataTableColumn<TItem>> Columns => columns;

        internal void AddColumn(MDCDataTableColumn<TItem> column)
        {
            columns.Add(column);
            StateHasChanged();
        }

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
