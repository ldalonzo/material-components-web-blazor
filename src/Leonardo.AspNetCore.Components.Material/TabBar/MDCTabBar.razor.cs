using Microsoft.AspNetCore.Components;
using System.Text;

namespace Leonardo.AspNetCore.Components.Material.TabBar
{
    /// <summary>
    /// Tabs organize and allow navigation between groups of content that are related and at the same level of hierarchy.
    /// The Tab Bar contains the Tab Scroller and Tab components.
    /// </summary>
    public partial class MDCTabBar
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        protected override StringBuilder BuildClassString(StringBuilder sb)
        {
            sb.Append("mdc-tab-bar");

            return base.BuildClassString(sb);
        }

        internal void AddTab(MDCTab tab)
        {

        }

        internal bool IsActive(MDCTab tab)
        {
            return true;
        }
    }
}
