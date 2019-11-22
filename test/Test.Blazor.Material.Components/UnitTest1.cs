using Blazor.Material.Components.Button;
using LD.AspNetCore.Components.Testing;
using Shouldly;
using System.Linq;
using Xunit;

namespace Test.Blazor.Material.Components
{
    public class UnitTest1
    {
        TestHost host = new TestHost();

        [Fact]
        public void Test1()
        {
            var component = host.AddComponent<MDCButton>();

            component.Item.ShouldNotBeNull();
            component.Item.ClassString.ShouldNotBeNull();
            component.Item.ClassString.Split().Where(r => r == "mdc-button").ShouldHaveSingleItem();
        }
    }
}
