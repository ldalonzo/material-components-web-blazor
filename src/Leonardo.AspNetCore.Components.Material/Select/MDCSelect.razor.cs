using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leonardo.AspNetCore.Components.Material.Select
{
    /// <summary>
    /// MDC Select provides Material Design single-option select menus, using the MDC menu.
    /// The Select component is fully accessible, and supports RTL rendering.
    /// </summary>
    /// <seealso href="https://github.com/material-components/material-components-web/tree/master/packages/mdc-select"/>
    public abstract class MDCSelect : MaterialComponent
    {
        [Parameter] public string Label { get; set; }

        [Inject] private IJSRuntime JSRuntime { get; set; }

        protected ElementReference _MDCSelect;

        protected override StringBuilder BuildClassString(StringBuilder sb)
        {
            sb.Append("mdc-select");

            sb.Append(" mdc-select--filled");

            return base.BuildClassString(sb);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await Attach();
                await ListenToChange();
            }
        }

        private async Task Attach()
            => await JSRuntime.InvokeVoidAsync("MDCSelectComponent.attachTo", _MDCSelect, Id);

        private async Task ListenToChange()
            => await JSRuntime.InvokeVoidAsync("MDCSelectComponent.listenToChange", Id, DotNetObjectReference.Create(this));

        /// <summary>
        /// Set the index of the currently selected option.  Set to -1 if no option is currently selected.
        /// Changing this property will update the select element.
        /// </summary>
        protected async Task SetSelectedIndex(int selectedIndex)
            => await JSRuntime.InvokeVoidAsync("MDCSelectComponent.setSelectedIndex", Id, selectedIndex);

        /// <summary>
        /// Used to indicate when an element has been selected.
        /// This event also includes the <paramref name="value"/> of the item and the <paramref name="index"/>.
        /// </summary>
        [JSInvokable]
        public Task OnChange(MDCSelectEventDetail arg)
            => HandleOnChange(arg.Value, arg.Index);

        protected virtual Task HandleOnChange(string value, int index) => Task.CompletedTask;
    }

    public partial class MDCSelect<T> : MDCSelect
    {
        [Parameter] public IEnumerable<T> DataSource { get; set; }

        [Parameter] public T Value { get; set; }

        [Parameter] public EventCallback<T> ValueChanged { get; set; }

        [Parameter] public string DataValueMember { get; set; }

        [Parameter] public string DisplayTextMember { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (DataSource == null)
            {
                DataSource = Enumerable.Empty<T>();
            }

            if (string.IsNullOrWhiteSpace(LabelId))
            {
                LabelId = $"{Id}-label";
            }

            if (string.IsNullOrWhiteSpace(SelectedTextId))
            {
                SelectedTextId = $"{Id}-selected-text";
            }

            IncludeEmptyItem = !typeof(T).IsValueType;
            LabelClassString = BuildLabelClassString();
            LabelledBy = BuildLabelledBy();

            InitializeOptionsItems();
        }

        private string LabelId { get; set; }

        private string SelectedTextId { get; set; }

        private string LabelClassString { get; set; }

        private string BuildLabelClassString()
        {
            var sb = new StringBuilder("mdc-floating-label");

            if (!Equals(Value, default))
            {
                sb.Append(" mdc-floating-label--float-above");
            }

            return sb.ToString();
        }

        private string LabelledBy { get; set; }

        private string BuildLabelledBy()
        {
            var sb = new StringBuilder();

            sb.Append(LabelId);
            sb.Append(" ");
            sb.Append(SelectedTextId);

            return sb.ToString();
        }

        private bool IncludeEmptyItem { get; set; }

        private readonly IDictionary<string, T> optionsByDataValue = new Dictionary<string, T>();

        private Task OnValueChanged(T value)
        {
            Value = value;
            return ValueChanged.InvokeAsync(Value);
        }

        protected override async Task HandleOnChange(string dataValue, int index)
        {
            await base.HandleOnChange(dataValue, index);

            var value = GetDataSourceItemFrom(dataValue);
            if (!Equals(value, Value))
            {
                await OnValueChanged(value);
            }
        }

        private T GetDataSourceItemFrom(string dataValue)
            => optionsByDataValue.TryGetValue(dataValue, out var value) ? value : (default);

        private string BuildItemClassString(T item = default)
        {
            var sb = new StringBuilder("mdc-list-item");

            if (IsSelected(item))
            {
                sb.Append(" mdc-list-item--selected");
            }

            return sb.ToString();
        }

        private bool IsSelected(T item = default)
            => Equals(item, Value);

        private void InitializeOptionsItems()
        {
            optionsByDataValue.Clear();
            foreach (var option in DataSource)
            {
                optionsByDataValue.Add(GetItemDataValue(option), option);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await SetSelectedIndex(GetSelectedItemIndex());
            }
        }

        private int GetIndexOf(T target, int index = 0)
        {
            foreach (var option in DataSource)
            {
                if (Equals(target, option))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        private int GetSelectedItemIndex()
        {
            var indexOfValue = GetIndexOf(Value, IncludeEmptyItem ? 1 : 0);
            if (indexOfValue == -1)
            {
                return 0;
            }
            else
            {
                return indexOfValue;
            }
        }

        private string GetItemDataValue(T item)
            => GetPropertyValueOrDefault(item, DataValueMember);

        private string GetItemDisplayText(T item)
            => GetPropertyValueOrDefault(item, DisplayTextMember);

        private static string GetPropertyValueOrDefault(T item, string propertyName)
        {
            if (item == null)
            {
                return string.Empty;
            }

            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                var propertyInfo = typeof(T).GetProperty(propertyName);
                return propertyInfo.GetValue(item) as string;
            }

            return item.ToString();
        }
    }
}
