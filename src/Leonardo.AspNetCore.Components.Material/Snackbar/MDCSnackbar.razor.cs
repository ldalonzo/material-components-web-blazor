using Microsoft.AspNetCore.Components;
using System.Text;

namespace Leonardo.AspNetCore.Components.Material.Snackbar
{
    /// <summary>
    /// Snackbars provide brief messages about app processes at the bottom of the screen.
    /// </summary>
    public partial class MDCSnackbar
    {
        [Parameter] public string Text { get; set; }

        [Parameter] public string ButtonLabel { get; set; }

        protected override StringBuilder BuildClassString(StringBuilder sb)
        {
            sb.Append("mdc-snackbar");

            return base.BuildClassString(sb);
        }
    }
}
