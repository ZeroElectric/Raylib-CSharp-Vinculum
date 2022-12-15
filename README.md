# Raylib-CSharp-Vinculum [![Nuget](https://img.shields.io/nuget/v/Raylib-CSharp-Vinculum)](https://www.nuget.org/packages/Raylib-CSharp-Vinculum/) [![Source Code](https://img.shields.io/badge/100+_Examples-blueviolet)](https://github.com/ZeroElectric/Raylib-CSharp-Vinculum/blob/main/Source/Raylib-CSharp-Vinculum.Examples) ![.NET 5+](https://img.shields.io/badge/.NET-net5+-%23512bd4) ![Platforms](https://img.shields.io/badge/Platforms-Win--x64-blue) ![GitHub](https://img.shields.io/github/license/ZeroElectric/Raylib-CSharp-Vinculum) 

### **Raylib-CSharp-Vinculum is a autogen C#/.Net binding for [Raylib](https://github.com/raysan5/raylib), a simple and easy-to-use 2d/3d videogame framework, similar to XNA / MonoGame.**

  - Windows supported. With Linux planned.
  - Supports .Net 5+, Mono 6.4+, Net Core3.0
  - 1-1 bindings + convenience wrappers to make it easier to use.
  - Includes bindings for all of Raylib's extras:
    - `raylib` : Core features, including Audio.
    - `rlgl` : OpenGl abstraction
    - `raygui` : An Imperitive Gui
    - `physac` : A simple 2d physics framework.
    - `rres` : A simple and easy-to-use file-format to package resources
    - `easings` : Use for simple animations  (C# Managed Port)
    - `raymath` : A game math library (C# Managed Port)
  - Requires `unsafe` for 3d workflows.
  - A focus on performance. No runtime allocations if at all possible.
  - A fork of [Raylib-CsLo](https://github.com/NotNotTech/Raylib-CsLo) as the maintainer wishes to step down.
  - No intellisense docs. [You can read the Raylib cheatsheet for some help](https://www.raylib.com/cheatsheet/cheatsheet.html) or [view the examples]
  - Nuget package can be found [here](https://www.nuget.org/packages/Raylib-CSharp-Vinculum/)
  - Go give Ray some love, https://github.com/sponsors/raysan5

## Wait a minute haven't I seen this repository before?
Maybe! This repo is a fork of [Raylib-CsLo](https://github.com/NotNotTech/Raylib-CsLo) and the Maintainer (jasonswearingen/Novaleaf) announced they wanted to step down from the project and seeing as I use the project for a set a game-making tools I decided to fork the project and greatly optimize the project layout for better long term maintainability. Why did I change the name? Honestly the name `Raylib-CsLo` is kinda boring, and being inspired by projects with names like [Vortice](https://github.com/amerkoleci/Vortice.Windows) I chose the name `Vinculum vin·cu·lum` witch means `Bond` in Latin also I dont want to "steal" the name, other then the name, the only real change from a end-user point of view is namespace is different `Raylib-CsLo > ZeroElectric.Vinculum`

## What is **Raylib**?

[Raylib](https://www.raylib.com/) is a easey-to-use videogame framework well suited for prototyping, tooling, embedded systems and education, inclueds systems for: audio, 3D, 2D, 2D physics, fonts, animation, an OpenGL abstraction layer & more. Inspired By `Xna` & The `Borland Graphics Interface`.  **However, `Raylib` is a C framework**.  `Raylib-CSharp-Vinculum` is a C# autogen wrapper, which lets you use Raylib in C#/.Net.

## High performance for 3D! (but `unsafe` to use)

3D requires the `unsafe` keyword.  If you use 3D, you will need to understand how pointers work. A basic guide on pointers can be found [here](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/unsafe-code).

### Additionally, 3D users: **be sure you check the FAQ & Tips section below**, especially on how you need to use `Matrix4x4.Transpose()` when sending Matricies to Raylib.

# How to Install

## via Nuget

- ### By adding the package using .NET CLI:

      dotnet add package Raylib-CSharp-Vinculum --version 4.2.0-alpha 

- ### Or by searching for [Raylib-CSharp-Vinculum](https://www.nuget.org/packages/Raylib-CSharp-Vinculum/) in Visual Studio's Nuget Package Manager

## via Source

### Prerequisites
- Currently only buildable on Windows 10/11
- Visual Studio 2022 with the following workloads:
  - .NET SDK (NET6+)
  - Visual C++ Toolset
  - MSVC v142(or higher) x64/x86

1. Clone this repo using the git command below. **Downloading this repo as a zip file will not work** as it is important you use `git clone --recursive` to get all of the submodules
```
git clone --recursive https://github.com/ZeroElectric/Raylib-CSharp-Vinculum.git
```
> If you didn't/forgot to use `--recursive`, you can run `git submodule update --init --recursive` to fix it
2. Run `build.bat` and wait for the build to complete 
3. Reference `Raylib-CSharp-Vinculum.dll` from `Output\bin` and import the folder `runtimes` into your project's root directory
4. If you're looking for the compiled 'Examples' it will be in `Output\example-bin`

>If the build wasn't successful make a new issue with the error it gave you

# Differences from [Raylib-CsLo](https://github.com/NotNotTech/Raylib-CsLo)

- Changed the namespace from `Raylib-CsLo > ZeroElectric.Vinculum`
- Greatly optimized the project layout
- Uses a newer build of Raylib
- New Build system, now you just run build.bat from the root to build the lib

# Examples

Basic Example:
---
```cs
using ZeroElectric.Vinculum;

namespace VinculumExample
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      Raylib.InitWindow(1280, 720, "Hello, Raylib-Vinculum");
      Raylib.SetTargetFPS(60);
      
      // Main game loop
      while (!Raylib.WindowShouldClose()) // Detect window close button or ESC key
      {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Raylib.SKYBLUE);
       
        Raylib.DrawFPS(10, 10);
        
        Raylib.DrawText("Hello Raylib in CSharp!", 640 , 360, 50, Raylib.BLUE);
        
        Raylib.EndDrawing();
      }

      Raylib.CloseWindow();
    }
  }
}
```

# FAQ & Tips

- **How do I convert a string to `sbyte*` or vice-versa?**
  - All API's that take `sbyte*` have `string` wrappers, so be sure to look at the overload you can call.
- **Do I have to really cast my Enum to `int`?**
  - The autogen bindings are left untouched, however convenience wrappers have been added.  Usually these will automatically "work" via function overloads, but when this is not possible, try adding an underscore `_` to the end of the function/property.  For example:  
    ```
    Camera3D.projection_ = CameraProjection.CAMERA_ORTHOGRAPHIC;
    
    Gesture gesture = Raylib.GetGestureDetected_();
    ```
  - If all else fails, yes.  Cast to `(int)`.
- **I ran the Example project in a profiler. What are all these `sbyte[]` arrays being allocated?**
  - A pool of `sbyte[]` is allocated for string marshall purposes, to avoid runtime allocations.
- **Can I, Should I use `RayMath`?**
  - `ZeroElectric.Vinculum.RayMath` contains a lot of super helpful functions for doing gamedev related maths.
  - The `RayMath` helper functions have been translated into C# code.   This makes the code pretty fast, but if the same function exists under `System.Numerics` you should use that instead, because the DotNet CLR treats things under System.Numerics special, and optimizes it better.
- **Why are my matricies corrupt?**
  - Raylib/OpenGl uses column-major matricies, while dotnet/vulkan/directx uses row-major.  When passing your final calculated matrix to raylib for rendering, call `Matrix4x4.Transpose(yourMatrix)`

# Known Issues

- `RayGui`: be sure to call `RayGui.GuiLoadStyleDefault();` right after you `InitWindow()`.  This is needed to initialize the gui properly.  If you don't, if you close a raylib window and then open a new one (inside the same app), the gui will be broken.
- The `Text.Unicode` example doesn't render unicode properly.  Maybe the required font is missing, maybe there is a bug in the example (Utf16 to Utf8 conversion) or maybe there is a bug in Raylib.  A hunch: I think it's probably due to the fonts not including unicode characters, but I didn't investigate further.
- Native Memory allocation functions are not ported:  use `System.Runtime.InteropServices.NativeMemory.Alloc()` instead
- `LogCustom()` is ported but doesn't support variable length arguments.
- `Texture2D` doesn't exist.  it is just an alias for `Texture` so use that instead.  
- You might want to use `using` aliases like the following
    ```cs
    //Use 'global using'-'global using static' to make C# code function more like the raylib c examples.   
    global using static ZeroElectric.Vinculum.Raylib;
    global using static ZeroElectric.Vinculum.RayMath;
    global using static ZeroElectric.Vinculum.RayGui;
    global using static ZeroElectric.Vinculum.RlGl;

    global using Camera = ZeroElectric.Vinculum.Camera3D;
    global using RenderTexture2D = ZeroElectric.Vinculum.RenderTexture;
    global using Texture2D = ZeroElectric.Vinculum.Texture;
    global using TextureCubemap = ZeroElectric.Vinculum.Texture;
    global using Matrix = System.Numerics.Matrix4x4;
    ```

# License
## Mozilla Public License 2.0 (**MPL**)

By default, this repository is licensed under the [Mozilla Public License 2.0 (**MPL**)](https://github.com/ZeroElectric/Raylib-CSharp-Vinculum/blob/main/LICENSE).  The MPL is a popular "weak copyleft" license that allows just about anything.  **For example, you may use/include/static-link this library in a commercial, closed-source project without any burdens.**    The main limitation of the MPL being that: ***Modifications to the source code in this project must be open sourced***.  

The MPL is a great choice, both by providing flexibility to the user, and by encouraging contributions to the underlying project.  If you would like to read about the MPL, **FOSSA** has [a great overview of the MPL 2.0 here](https://fossa.com/blog/open-source-software-licenses-101-mozilla-public-license-2-0/).
