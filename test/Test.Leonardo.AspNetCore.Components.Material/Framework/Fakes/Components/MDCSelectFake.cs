using Leonardo.AspNetCore.Components.Material.Select;
using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Foundations;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components
{
    public class MDCSelectFake : MDCComponentFake<MDCSelectFoundation>
    {
        public int SelectedIndex { get; set; }

        public Task SetSelectedIndex(int index, string value)
        {
            SelectedIndex = index;
            return Emit("MDCSelect:change", new MDCSelectEventDetail { Value = value, Index = index });
        }
    }
}
