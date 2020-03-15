using Microsoft.AspNetCore.Components;
namespace Leonardo.AspNetCore.Components.Material.DataTable
{
    public partial class MDCDataTableColumn
    {
        [CascadingParameter] public MDCDataTable DataTable { get; set; }

        [Parameter] public string Header { get; set; }

        protected override void OnInitialized()
        {
            DataTable.AddColumn(this);
        }
    }
}
