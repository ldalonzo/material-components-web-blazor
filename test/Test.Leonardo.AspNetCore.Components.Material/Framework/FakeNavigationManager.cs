using Microsoft.AspNetCore.Components;
using System;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public class FakeNavigationManager : NavigationManager
    {
        public FakeNavigationManager()
        {
            var baseUri = new Uri("http://localhost");
            var uri = new Uri("http://localhost/test");

            Initialize(baseUri.ToString(), uri.ToString());
        }

        protected override void NavigateToCore(string uri, bool forceLoad)
        {
        }
    }
}
