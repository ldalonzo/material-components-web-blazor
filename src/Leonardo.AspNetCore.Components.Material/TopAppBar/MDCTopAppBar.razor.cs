using System.Text;

namespace Leonardo.AspNetCore.Components.Material.TopAppBar
{
    /// <summary>
    /// MDC Top App Bar acts as a container for items such as application title, navigation icon, and action items.
    /// </summary>
    /// <seealso href="https://material.io/develop/web/components/top-app-bar/"/>
    public partial class MDCTopAppBar : MaterialComponent
    {
        protected override string BuildClassString()
        {
            var sb = new StringBuilder("mdc-top-app-bar");

            if (!string.IsNullOrWhiteSpace(Class))
            {
                sb.Append($" {Class}");
            }

            return sb.ToString();
        }
    }
}
