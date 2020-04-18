using Microsoft.AspNetCore.Components;
using System.Text;

namespace Leonardo.AspNetCore.Components.Material.DataTable
{
    public partial class MDCDataTableColumn<TItem>
    {
        [CascadingParameter] public MDCDataTable<TItem> DataTable { get; set; }

        [Parameter] public string Header { get; set; }

        [Parameter] public string DataMember { get; set; }

        [Parameter] public bool Numeric { get; set; }

        [Parameter] public RenderFragment<TItem> DataTemplate { get; set; }

        protected override void OnInitialized()
            => DataTable.AddColumn(this);

        public string GetHeaderCssClass()
        {
            var sb = new StringBuilder();

            sb.Append("mdc-data-table__header-cell");

            if (Numeric)
            {
                sb.Append(" mdc-data-table__header-cell--numeric");
            }

            return sb.ToString();
        }

        public string GetCellCssClass()
        {
            var sb = new StringBuilder();

            sb.Append("mdc-data-table__cell");

            if (Numeric)
            {
                sb.Append(" mdc-data-table__cell--numeric");
            }

            return sb.ToString();
        }
    }
}
