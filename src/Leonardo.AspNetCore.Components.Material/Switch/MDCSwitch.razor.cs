using System.Text;

namespace Leonardo.AspNetCore.Components.Material.Switch
{
    public partial class MDCSwitch
    {
        protected override StringBuilder BuildClassString(StringBuilder sb)
        {
            sb.Append("mdc-switch");

            return base.BuildClassString(sb);
        }
    }
}
