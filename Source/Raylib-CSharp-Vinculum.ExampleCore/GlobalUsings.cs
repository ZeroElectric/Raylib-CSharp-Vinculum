
//------------------------------------------------------------------------------
//
// Copyright 2022-2023 (C) Raylib-CSharp-Vinculum, Raylib-CsLo and Contributors. 
// This file is licensed to you under the MPL-2.0.
// See the LICENSE file in the project's root for more info.
//
// Raylib-CSharp-Vinculum, .Net/C# bindings for raylib 5.0.
// Find Raylib-CSharp-Vinculum here: https://github.com/ZeroElectric/Raylib-CSharp-Vinculum
// Find raylib here: https://github.com/raysan5/raylib
//
//------------------------------------------------------------------------------

/////////////////////////////////////////
// All these `global using` statements are for convience in porting the examples, to allow the examples to be copy-pasted almost verbatum.

global using System.Numerics;
global using ZeroElectric.Vinculum.Extensions;

global using static ZeroElectric.Vinculum.RayGui;
global using static ZeroElectric.Vinculum.Raylib;
global using static ZeroElectric.Vinculum.RayMath;
global using static ZeroElectric.Vinculum.Easings;
global using static ZeroElectric.Vinculum.RlGl;
global using static ZeroElectric.Vinculum.Physac;

global using static ZeroElectric.Vinculum.BlendMode;
global using static ZeroElectric.Vinculum.CameraMode;
global using static ZeroElectric.Vinculum.CameraProjection;
global using static ZeroElectric.Vinculum.ConfigFlags;
global using static ZeroElectric.Vinculum.CubemapLayout;
global using static ZeroElectric.Vinculum.FontType;
global using static ZeroElectric.Vinculum.GamepadAxis;
global using static ZeroElectric.Vinculum.GamepadButton;
global using static ZeroElectric.Vinculum.Gesture;
global using static ZeroElectric.Vinculum.KeyboardKey;
global using static ZeroElectric.Vinculum.MaterialMapIndex;
global using static ZeroElectric.Vinculum.MouseButton;
global using static ZeroElectric.Vinculum.MouseCursor;
global using static ZeroElectric.Vinculum.NPatchLayout;
global using static ZeroElectric.Vinculum.PixelFormat;
global using static ZeroElectric.Vinculum.rlFramebufferAttachTextureType;
global using static ZeroElectric.Vinculum.rlFramebufferAttachType;
global using static ZeroElectric.Vinculum.rlShaderLocationIndex;
global using static ZeroElectric.Vinculum.ShaderLocationIndex;
global using static ZeroElectric.Vinculum.ShaderUniformDataType;
global using static ZeroElectric.Vinculum.TextureFilter;
global using static ZeroElectric.Vinculum.TextureWrap;
global using static ZeroElectric.Vinculum.TraceLogLevel;

//
// The following `global using` statements are for the examples that use RLights.

global using ZeroElectric.Vinculum.ExampleCore.Other;
global using static ZeroElectric.Vinculum.ExampleCore.Other.RLights;
global using static ZeroElectric.Vinculum.ExampleCore.Other.RLights.LightType;

/////////////////////////////////////////
// The C examples use type aliases, so the following `global using` lines are to match those.

global using Matrix = System.Numerics.Matrix4x4;
global using Camera = ZeroElectric.Vinculum.Camera3D;
global using Texture2D = ZeroElectric.Vinculum.Texture;
global using TextureCubemap = ZeroElectric.Vinculum.Texture;
global using RenderTexture2D = ZeroElectric.Vinculum.RenderTexture;
