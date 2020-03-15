using System.Text;

namespace Leonardo.AspNetCore.Components.Material.DataTable
{
    /// <summary>
    /// Data tables display information in a way that’s easy to scan, so that users can look for patterns and insights.
    /// </summary>
    public partial class MDCDataTable
    {
        protected override string BuildClassString()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(Class))
            {
                sb.Append($" {Class}");
            }

            return sb.ToString();
        }
    }
}
