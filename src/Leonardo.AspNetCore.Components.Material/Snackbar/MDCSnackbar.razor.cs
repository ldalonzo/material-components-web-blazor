using System.Text;

namespace Leonardo.AspNetCore.Components.Material.Snackbar
{
    /// <summary>
    /// Snackbars provide brief messages about app processes at the bottom of the screen.
    /// </summary>
    public partial class MDCSnackbar
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
