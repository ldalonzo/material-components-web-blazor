using Microsoft.AspNetCore.Components;
using Shouldly;
using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop
{
    internal class MDCListJsIteropFake : MDCComponentJsInterop<MDCList>
    {
        protected override string ComponentIdentifier => "MDCListComponent";

        public override Task AttachTo(object[] args)
        {
            args.Length.ShouldBe(2);
            var elementRef = args[0].ShouldBeOfType<ElementReference>();
            elementRef.Id.ShouldNotBeNullOrWhiteSpace();

            var wrapFocus = args[1].ShouldBeOfType<bool>();

            componentsById[elementRef.Id] = new MDCList { WrapFocus = wrapFocus };

            return Task.CompletedTask;
        }
    }
}
