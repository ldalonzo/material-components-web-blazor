using Microsoft.AspNetCore.Components;

namespace Leonardo.AspNetCore.Components.Material.DataTable
{
    public partial class MDCDataTableColumn<TItem>
    {
        [CascadingParameter] public MDCDataTable<TItem> DataTable { get; set; }

        [Parameter] public string Header { get; set; }

        [Parameter] public string DataMember { get; set; }

        [Parameter] public RenderFragment<TItem> DataTemplate { get; set; }

        protected override void OnInitialized()
            => DataTable.AddColumn(this);
    }
}
