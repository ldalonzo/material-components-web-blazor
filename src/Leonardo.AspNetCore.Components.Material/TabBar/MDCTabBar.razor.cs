using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Leonardo.AspNetCore.Components.Material.TabBar
{
    /// <summary>
    /// Tabs organize and allow navigation between groups of content that are related and at the same level of hierarchy.
    /// The Tab Bar contains the Tab Scroller and Tab components.
    /// </summary>
    public partial class MDCTabBar
    {
        [Parameter] public RenderFragment ChildContent { get; set; }

        [Inject] private IJSRuntime JSRuntime { get; set; }

        protected ElementReference _MDCTabBar;

        private readonly List<MDCTab> tabs = new List<MDCTab>();

        protected override StringBuilder BuildClassString(StringBuilder sb)
        {
            sb.Append("mdc-tab-bar");

            return base.BuildClassString(sb);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("MDCTabBarComponent.attachTo", _MDCTabBar, Id);
                await JSRuntime.InvokeVoidAsync("MDCTabBarComponent.listenToActivated", Id, DotNetObjectReference.Create(this));
            }
        }

        [JSInvokable]
        public Task OnTabActivated(MDCTabBarActivatedEventDetail detail)
        {
            var activeTab = tabs[detail.Index];
            return InvokeAsync(() => SetActivateTab(activeTab));
        }

        private MDCTab ActiveTab { get; set; }

        internal void AddTab(MDCTab tab)
        {
            tabs.Add(tab);

            if (ActiveTab == null)
            {
                SetActivateTab(tab);
            }
        }

        internal bool IsActive(MDCTab tab)
            => tab != null && tab == ActiveTab;

        private void SetActivateTab(MDCTab tab)
        {
            if (ActiveTab != tab)
            {
                ActiveTab = tab;
                StateHasChanged();
            }
        }
    }
}
