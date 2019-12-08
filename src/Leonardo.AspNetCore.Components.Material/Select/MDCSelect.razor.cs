using System.Text;

namespace Leonardo.AspNetCore.Components.Material.Select
{
    public partial class MDCSelect : MaterialComponent
    {
        protected override string BuildClassString()
        {
            var sb = new StringBuilder();

            sb.Append("mdc-select");

            if (!string.IsNullOrWhiteSpace(Class))
            {
                sb.Append($" {Class}");
            }

            return sb.ToString();
        }
    }
}