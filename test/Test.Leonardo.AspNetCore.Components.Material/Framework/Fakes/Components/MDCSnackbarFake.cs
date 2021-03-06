﻿using Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Foundations;

namespace Test.Leonardo.AspNetCore.Components.Material.Framework.Fakes.Components
{
    internal class MDCSnackbarFake : MDCComponentFake<MDCSnackbarFoundation>
    {
        public void Open()
        {
            IsOpen = true;
        }

        public bool IsOpen { get; private set; }

        public string LabelText { get; set; }
    }
}
