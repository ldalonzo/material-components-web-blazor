﻿using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes;
using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.JSInterop
{
    internal class MDCCircularProgressJsInteropFake : MDCComponentJsInterop<MDCCircularProgressFake>
    {
        protected override string ComponentIdentifier => "MDCCircularProgressComponent";

        public Task SetDeterminate(object[] args)
        {
            args.Length.ShouldBe(2);

            var foundation = FindComponentById(args[0]);
            foundation.ShouldNotBeNull();

            foundation.Determinate = args[1].ShouldBeOfType<bool>();

            return Task.CompletedTask;
        }

        protected override IEnumerable<(string, Func<object[], Task>)> EnumerateFunctionsDefinitions()
        {
            yield return ("setDeterminate", SetDeterminate);
        }
    }
}
