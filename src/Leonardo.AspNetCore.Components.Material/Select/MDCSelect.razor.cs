using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
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

        [Inject] public IJSRuntime JSRuntime { get; set; }

        protected ElementReference mdcSelectElement;

        protected string Id { get; set; } = $"select-{Guid.NewGuid().ToString().Substring(0, 4).ToLower()}";

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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await Attach();
            }
        }

        private async Task Attach()
            => await JSRuntime.InvokeVoidAsync("MDCSelectComponent.attachTo", mdcSelectElement, DotNetObjectReference.Create(this));

        /// <summary>
        /// Set the index of the currently selected option.  Set to -1 if no option is currently selected.
        /// Changing this property will update the select element.
        /// </summary>
        protected async Task SetSelectedIndex(int selectedIndex)
            => await JSRuntime.InvokeVoidAsync("MDCSelectComponent.setSelectedIndex", mdcSelectElement, selectedIndex);

        /// <summary>
        /// Used to indicate when an element has been selected.
        /// This event also includes the <paramref name="value"/> of the item and the <paramref name="index"/>.
        /// </summary>
        [JSInvokable(nameof(OnChange))]
        public Task OnChange(string value, int index) => HandleOnChange(value, index);

        protected virtual Task HandleOnChange(string value, int index) => Task.CompletedTask;
    }

    public partial class MDCSelect<T> : MDCSelect
    {
        private readonly IDictionary<string, T> optionsByDataValue = new Dictionary<string, T>();

        [Parameter] public IEnumerable<T> DataSource { get; set; }

        [Parameter] public T Value { get; set; }

        [Parameter] public EventCallback<T> ValueChanged { get; set; }

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

        [Parameter] public string DataValueMember { get; set; }

        [Parameter] public string DisplayTextMember { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            if (DataSource == null)
            {
                DataSource = Enumerable.Empty<T>();
            }

            InitializeOptionsItems();
        }

        private void InitializeOptionsItems()
        {
            optionsByDataValue.Clear();
            foreach (var option in DataSource)
            {
                optionsByDataValue.Add(GetDataValue(option), option);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await SetSelectedIndex(GetSelectedItem());
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

        private int GetSelectedItem()
        {
            var indexOfValue = GetIndexOf(Value, 1);
            if (indexOfValue == -1)
            {
                return 0;
            }
            else
            {
                return indexOfValue;
            }
        }

        private string GetDataValue(T item) => GetPropertyValueOrDefault(item, DataValueMember);

        private string GetDisplayText(T item) => GetPropertyValueOrDefault(item, DisplayTextMember);

        private static string GetPropertyValueOrDefault(T item, string propertyName)
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                var propertyInfo = typeof(T).GetProperty(propertyName);
                return propertyInfo.GetValue(item) as string;
            }

            return item.ToString();
        }
    }
}
