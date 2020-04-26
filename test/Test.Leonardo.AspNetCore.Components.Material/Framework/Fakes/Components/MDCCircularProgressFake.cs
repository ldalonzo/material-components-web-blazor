using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Foundations;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components
{
    internal class MDCCircularProgressFake : MDCComponentFake<MDCCircularProgressFoundation>
    {
        public bool IsDeterminate { get; private set; }

        public bool Determinate
        {
            set
            {
                IsDeterminate = value;
            }
        }
    }
}
