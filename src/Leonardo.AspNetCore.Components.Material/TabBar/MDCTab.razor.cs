using Microsoft.AspNetCore.Components;
using System.Text;

namespace Leonardo.AspNetCore.Components.Material.TabBar
{
    /// <summary>
    /// Tabs organize and allow navigation between groups of content that are related and at the same level of hierarchy.
    /// The Tab Bar contains the Tab Scroller and Tab components.
    /// </summary>
    public partial class MDCTab
    {
        [Parameter] public string Label { get; set; }

        [Parameter] public string Icon { get; set; }

        [CascadingParameter] public MDCTabBar ContainerTabBar { get; set; }

        protected override StringBuilder BuildClassString(StringBuilder sb)
        {
            sb.Append("mdc-tab");

            if (IsActive)
            {
                sb.Append(" mdc-tab--active");
            }

            return base.BuildClassString(sb);
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            UpdateTabIndicatorClassString();
        }

        private string TabIndicatorClassString { get; set; }

        private string BuildTabIndicatorClassString()
        {
            var sb = new StringBuilder("mdc-tab-indicator");

            if (IsActive)
            {
                sb.Append(" mdc-tab-indicator--active");
            }

            return sb.ToString();
        }

        private void UpdateTabIndicatorClassString()
            => TabIndicatorClassString = BuildTabIndicatorClassString();

        private bool IsAriaSelected => IsActive;

        private bool IsActive => ContainerTabBar != null && ContainerTabBar.IsActive(this);

        protected override void OnInitialized()
        {
            base.OnInitialized();

            ContainerTabBar?.AddTab(this);
        }
    }
}
