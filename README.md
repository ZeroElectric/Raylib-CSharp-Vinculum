|  Raylib-CSharp-Vinculum  | ![.NET 5+](https://img.shields.io/badge/.NET-net5+-%23512bd4) [![Nuget](https://img.shields.io/nuget/v/Raylib-CSharp-Vinculum)](https://www.nuget.org/packages/Raylib-CSharp-Vinculum/) [![Source Code](https://img.shields.io/badge/100+_Examples-blueviolet)](https://github.com/ZeroElectric/Raylib-CSharp-Vinculum/blob/main/Source/Raylib-CSharp-Vinculum.Examples) ![Platforms](https://img.shields.io/badge/Platforms-Win--x64%2C%20Linux--x64%20-blue) ![GitHub](https://img.shields.io/github/license/ZeroElectric/Raylib-CSharp-Vinculum) | 
| :---: | :---: | 
| <img align="left" src="https://github.com/ZeroElectric/Raylib-CSharp-Vinculum/blob/main/.assets/raylib-csharp-vinculum_logo_256x256.png" width="256px"> | **Raylib-CSharp-Vinculum is a .Net/C# autogen [raylib](https://github.com/raysan5/raylib) binding, raylib is a simple and easy-to-use 2d/3d videogame framework, similar to XNA & MonoGame.** | 

  - Windows & Linux supported,
  - Supports .Net 5+, Mono 6.4+, Core 3.0,
  - 1-1 bindings + convenience wrappers to make it easier to use,
  - Includes bindings for all of raylib's extras:
    - `raylib` : Core features, including Audio,
    - `rlgl` : OpenGl abstraction,
    - `raygui` : An immediate mode GUI framework,
    - `physac` : A simple 2d physics framework,
    - `rres` : A simple and easy-to-use file-format to package resources,
    - `easings` : Use for simple animations (C# Managed Port),
    - `raymath` : A game math library (C# Managed Port),
    - `rcamera` : A basic camera system (Direct C# port of rcamera.h).
  - Requires `unsafe` keyword for 3d workflows. A basic guide on pointers can be found [here](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/unsafe-code), 
  - A focus on performance. No runtime allocations if at all possible,
  - A fork of [Raylib-CsLo](https://github.com/NotNotTech/Raylib-CsLo) as the maintainer wishes to step down,
  - Very little intellisense docs. [You can read the Raylib cheatsheet for some help](https://www.raylib.com/cheatsheet/cheatsheet.html) or [view the examples](https://github.com/ZeroElectric/Raylib-CSharp-Vinculum/tree/main/Source/Raylib-CSharp-Vinculum.ExampleCore)
  - Go give Ray some love ❤️, https://github.com/sponsors/raysan5

> **Warning**:
> 3D users: **be sure you check the FAQ & Tips section below**, 
> especially on how you need to use `Matrix4x4.Transpose()` when sending Matricies
> to raylib.

> **Note:**
> Users comming from [Raylib-CsLo](https://github.com/NotNotTech/Raylib-CsLo) make sure to 
> read the `Differences` section below

## Wait a minute haven't I seen this repository before?
Maybe! This repo is a fork of [Raylib-CsLo](https://github.com/NotNotTech/Raylib-CsLo), the Maintainer (jasonswearingen/Novaleaf) announced they wished to step down from the project and seeing as I use the project for a set a game-making tools I decided to fork the project and greatly optimize the project layout for better long term maintainability. Why did I change the name? Honestly the name `Raylib-CsLo` is kinda boring, and being inspired by projects with names like [Vortice](https://github.com/amerkoleci/Vortice.Windows), I chose the name `Vinculum [vin·cu·lum]` witch means **bond** in Latin, also I didn't want to "steal" the name, other then the name, the only real change from a end-user point of view is a namespace difference `Raylib-CsLo > ZeroElectric.Vinculum`

## What is **Raylib**?

[raylib](https://www.raylib.com/) is a easey-to-use videogame framework well suited for prototyping, tooling, embedded systems and education, includes systems for: audio, 3D, 2D, 2D physics, fonts, animation, an OpenGL abstraction layer & more. Inspired By `XNA` & The `Borland Graphics Interface`.  **However, `raylib` is a C framework**, `Raylib-CSharp-Vinculum` is a .Net/C# autogen wrapper, which lets you use raylib in .Net.

# How to Install

## 📦 via Nuget 

- ### By adding the package using the .NET CLI:

      dotnet add package Raylib-CSharp-Vinculum

- ### Or by searching for [Raylib-CSharp-Vinculum](https://www.nuget.org/packages/Raylib-CSharp-Vinculum/) in Visual Studio's Nuget Package Manager

## 🏗️ via Source

> **Attention:**
> The current build system is only usable on Windows 10/11, this is temporary.

<details>

<summary>Building for Windows</summary>

### Prerequisites

- Visual Studio 2022 with the following workloads:
  - .NET SDK (NET6+)
  - Visual C++ Toolset
  - MSVC v142 (or higher) x64/x86

1. Clone this repo using the git command below. **(Note: Downloading this repo as a zip file will not work,** it is important you use `git clone --recursive` to get all of the submodules)
```
git clone --recursive https://github.com/ZeroElectric/Raylib-CSharp-Vinculum.git
```
> **Note:** 
> If you didn't or forgot to use `--recursive`, you can run `git submodule update --init --recursive` to fix it

- Run `build.bat` and wait for the build to complete 
- Reference `Raylib-CSharp-Vinculum.dll` from `Output\bin` and import the folder `runtimes` into your project's root directory
- Compiled 'Examples' will be in `Output\example-bin`

>If the build wasn't successful make a new issue with the error it gave you

</details>

# Differences from [Raylib-CsLo](https://github.com/NotNotTech/Raylib-CsLo)

### The only real changes from a end-user point of view is a namespace difference `Raylib-CsLo > ZeroElectric.Vinculum` & uses newer build of `raylib` that might have removed or changed some functions you use.

#### Other changes:
- Greatly optimized the project layout,
- New build system, now you just run build.bat from the root folder to build the library.

# Examples

Basic Example:
---
```csharp
using ZeroElectric.Vinculum;

namespace VinculumExample;

public static class Program
{
	public static void Main(string[] args)
	{
		// Set the width and height of the window     
		int screenWidth = 800;
		int screenHeight = 450;

		// Initialize the window with the specified width, height, and title
		Raylib.InitWindow(screenWidth, screenHeight, "Hello World , Raylib-CSharp-Vinculum");

		// Set the FPS to 60
		Raylib.SetTargetFPS(60);

		// Loop until the window is closed
		while (!Raylib.WindowShouldClose())
		{
			// Begin drawing to the window
			Raylib.BeginDrawing();

			// Clear the background to white
			Raylib.ClearBackground(Color.RAYWHITE);

			// Draw the text "Hello World" in maroon color at position (190, 200)
			Raylib.DrawText("Hello Raylib in CSharp!", 190, 200, 20, Color.MAROON);

			// End drawing to the window
			Raylib.EndDrawing();
		}

		// Close the window
		Raylib.CloseWindow();
	}
}
```

- View code example collection [here](https://github.com/ZeroElectric/Raylib-CSharp-Vinculum/tree/main/Source/Raylib-CSharp-Vinculum.ExampleCore)
- raylib 4.5 cheatsheet [here](https://www.raylib.com/cheatsheet/cheatsheet.html)
- raylib architecture design [here](https://github.com/raysan5/raylib/wiki/raylib-architecture)

# ❓ FAQ & Tips

### **Make sure your matricies aren't corrupt!**
  - Raylib is built upon OpenGL which uses column-major matricies, while .Net uses row-major. When passing your final calculated matricies to raylib for rendering, call `Matrix4x4.Transpose(yourMatrix)` first.

### **How do I convert a string to `sbyte*`?**
  - Most methods that take `sbyte*` have a `string` wrapper, so be sure to look at the overloads you can call.
  - If you still with to convert a string, use `SpanOwner<T>` from the `CommunityToolkit.HighPerformance` Nuget package and `MarshalUtf8()` & `AsPtr()` from the `ZeroElectric.Vinculum.Extensions` namspace , for example:
    ```csharp
    using CommunityToolkit.HighPerformance.Buffers;
    using ZeroElectric.Vinculum.Extensions; 

    string mytext = "Here be some text";

    using SpanOwner<sbyte> spanOwner = mytext.MarshalUtf8();
    DrawText(spanOwner.AsPtr(), 10, 10, 10, BLACK);
    ```

### **Do I have to cast enums to `int`?**
  - The autogen bindings are left untouched, however convenience wrappers have been added.  Usually these will automatically "work" via method overloads, but when this is not possible, try adding an underscore `_` to the end of the method or property, for example:  
    ```csharp
    Camera3D.projection_ = CameraProjection.CAMERA_ORTHOGRAPHIC;
    
    Gesture gesture = Raylib.GetGestureDetected_();
    ```
  - If all else fails, yes, cast to `int`.

 ### **Can or Should I use `RayMath`?**
  - `ZeroElectric.Vinculum.RayMath` contains a lot of helpful methods for doing game related math.
  - The `RayMath` helper methods have been translated into C#, this makes the code pretty fast, but if the same method exists under `System.Numerics` you should use that instead, because the .Net CLR treats things under System.Numerics special, and optimizes it better.

### **I ran the Example project in a profiler. What are all these `sbyte[]` arrays being allocated?**
  - A pool of `sbyte[]` arrays are allocated for string marshalling purposes, to avoid runtime allocations.

### TIP: You might want to use the `global using directive` to create aliases like the following to make C# code function more like the raylib C examples.
  ```csharp   
  global using static ZeroElectric.Vinculum.Raylib;
  global using static ZeroElectric.Vinculum.RayMath;
  global using static ZeroElectric.Vinculum.RayCamera;
  global using static ZeroElectric.Vinculum.RayGui;
  global using static ZeroElectric.Vinculum.RlGl;

  global using Camera = ZeroElectric.Vinculum.Camera3D;
  global using RenderTexture2D = ZeroElectric.Vinculum.RenderTexture;
  global using Texture2D = ZeroElectric.Vinculum.Texture;
  global using TextureCubemap = ZeroElectric.Vinculum.Texture;
  global using Matrix = System.Numerics.Matrix4x4;
  ```

# Known Issues

- When using `RayGui`, if you close a raylib window after calling `RayGui.GuiLoadStyleDefault()` and then open a new raylib window (within the same running instance), multiple rayGui ui elements will be broken, 
- `Texture2D` doesn't exist, it's just an alias for `Texture` so use that instead,
- `LogCustom()` is ported but doesn't support variable length arguments,
- The Linux build supplied by the nuget pakage was built under `debug` mode,
- The `Text.Unicode` example doesn't render unicode properly.

# License
## Mozilla Public License 2.0 (**MPL**)

This repository is licensed under the [Mozilla Public License 2.0 (**MPL**)](https://github.com/ZeroElectric/Raylib-CSharp-Vinculum/blob/main/LICENSE).  The MPL is a popular "weak copyleft" license that allows just about anything.  **For example, you may use/include/static-link this library in a commercial, closed-source project without any burdens.**    The main limitation of the MPL being that: ***Modifications to the source code in this project must be open sourced***.  

The MPL is a great choice, both by providing flexibility to the user, and by encouraging contributions to the underlying project.  If you would like to read about the MPL, **FOSSA** has [a great overview of the MPL 2.0 here](https://fossa.com/blog/open-source-software-licenses-101-mozilla-public-license-2-0/).
