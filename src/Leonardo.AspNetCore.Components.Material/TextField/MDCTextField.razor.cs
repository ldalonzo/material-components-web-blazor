using System.Text;

namespace Leonardo.AspNetCore.Components.Material.TextField
{
    /// <summary>
    /// Text fields allow users to input, edit, and select text.
    /// </summary>
    /// <seealso href="https://material.io/develop/web/components/input-controls/text-field/"/>
    public partial class MDCTextField : MaterialComponent
    {
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
