[![CI](https://github.com/ldalonzo/material-components-web-blazor/actions/workflows/ci.yml/badge.svg)](https://github.com/ldalonzo/material-components-web-blazor/actions/workflows/ci.yml)
[![Version](https://img.shields.io/nuget/vpre/Leonardo.AspNetCore.Components.Material)](https://www.nuget.org/packages/Leonardo.AspNetCore.Components.Material/)
[![codecov](https://codecov.io/gh/ldalonzo/material-components-web-blazor/branch/develop/graph/badge.svg)](https://codecov.io/gh/ldalonzo/material-components-web-blazor)

# Material Components for Blazor WebAssembly
A collection of ASP.NET Core Razor components that wrap [Material Components for the web](https://github.com/material-components/material-components-web).

- [Live Demo](https://material-components-blazor.azurewebsites.net/)

## Quick start

### Step 1: Add NuGet package
```powershell
dotnet add package Leonardo.AspNetCore.Components.Material
```

### Step 2: Reference static assets
```html
<html>
  <head>
    <link rel="stylesheet" href="_content/Leonardo.AspNetCore.Components.Material/bundle.css" />
  </head>

  <body>
    <script src="_content/Leonardo.AspNetCore.Components.Material/app.bundle.js"></script>
    <script src="_content/Leonardo.AspNetCore.Components.Material/checkbox.bundle.js"></script>
    <script src="_content/Leonardo.AspNetCore.Components.Material/circular-progress.bundle.js"></script>
    <script src="_content/Leonardo.AspNetCore.Components.Material/drawer.bundle.js"></script>
    <script src="_content/Leonardo.AspNetCore.Components.Material/ripple.bundle.js"></script>
    <script src="_content/Leonardo.AspNetCore.Components.Material/snackbar.bundle.js"></script>
    <script src="_content/Leonardo.AspNetCore.Components.Material/switch.bundle.js"></script>
    <script src="_content/Leonardo.AspNetCore.Components.Material/tab-bar.bundle.js"></script>
    <script src="_content/Leonardo.AspNetCore.Components.Material/textfield.bundle.js"></script>
    <script src="_content/Leonardo.AspNetCore.Components.Material/top-app-bar.bundle.js"></script>
  </body>
</html>
```
