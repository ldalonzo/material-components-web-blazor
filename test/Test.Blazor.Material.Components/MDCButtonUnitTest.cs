using Blazor.Material.Components.Button;
using LD.AspNetCore.Components.Testing;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Test.Blazor.Material.Components
{
    public class MDCButtonUnitTest
    {
        TestHost host = new TestHost();

        [Fact]
        public void TestCreation()
        {
            var component = host.AddComponent<MDCButton>();
            component.Item.ShouldNotBeNull();
        }

        [Fact]
        public void TestCssClasses()
        {
            var component = host.AddComponent<MDCButton>();

            component.Item.ClassString.ShouldNotBeNull();
            component.Item.ClassString.Split().Where(r => r == "mdc-button").ShouldHaveSingleItem();
        }

        [Fact]
        public void TestCssClasses_VariantOutlined()
        {
            var component = host.AddComponent<MDCButton>(new Dictionary<string, object>() {
                {nameof(MDCButton.Variant), MDCButtonStyle.Outlined }
            });

            component.Item.ClassString.ShouldNotBeNull();
            component.Item.ClassString.Split().Where(r => r == "mdc-button").ShouldHaveSingleItem();
            component.Item.ClassString.Split().Where(r => r == "mdc-button--outlined").ShouldHaveSingleItem();
        }
    }
}
