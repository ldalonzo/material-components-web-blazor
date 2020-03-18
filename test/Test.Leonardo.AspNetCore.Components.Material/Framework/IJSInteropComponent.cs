using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test.Leonardo.AspNetCore.Components.Material
{
    public interface IJSInteropComponent
    {
        IDictionary<string, Func<object[], Task>> GetFunctionsDefinitions();
    }
}
