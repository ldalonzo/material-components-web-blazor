using Microsoft.AspNetCore.Components;

namespace Leonardo.AspNetCore.Components.Material
{
    public abstract class MaterialComponent : ComponentBase
    {
        [Parameter] public string Class { get; set; }

        protected string ClassString { get; private set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            ClassString = BuildClassString();
        }

        protected abstract string BuildClassString();
    }
}
