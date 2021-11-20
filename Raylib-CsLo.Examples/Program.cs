// [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] 
// [!!] Copyright ©️ Raylib-CsLo and Contributors. 
// [!!] This file is licensed to you under the LGPL-2.1.
// [!!] See the LICENSE file in the project root for more info. 
// [!!] ------------------------------------------------- 
// [!!] The code ane examples are here! https://github.com/NotNotTech/Raylib-CsLo 
// [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!]  [!!] [!!] [!!] [!!]

global using System.Numerics;

global using Raylib_CsLo;

global using static Raylib_CsLo.Raylib;
global using static Raylib_CsLo.RayMath;
global using static Raylib_CsLo.RayGui;
global using static Raylib_CsLo.RlGl;

global using static Raylib_CsLo.KeyboardKey;
global using static Raylib_CsLo.MouseButton;
global using static Raylib_CsLo.GamepadAxis;
global using static Raylib_CsLo.GamepadButton;
global using static Raylib_CsLo.Gesture;
global using Raylib_CsLo.InternalHelpers;
global using static Raylib_CsLo.ConfigFlags;
global using static Raylib_CsLo.CameraProjection;
global using static Raylib_CsLo.CameraMode;
global using static Raylib_CsLo.TextureFilter;
global using static Raylib_CsLo.TextureWrap;
global using static Raylib_CsLo.ShaderUniformDataType;
global using static Raylib_CsLo.TraceLogLevel;
global using static Raylib_CsLo.MaterialMapIndex;
global using static Raylib_CsLo.CubemapLayout;
global using static Raylib_CsLo.PixelFormat;

global using static Raylib_CsLo.rlFramebufferAttachType;
global using static Raylib_CsLo.rlFramebufferAttachTextureType;
global using static Raylib_CsLo.rlShaderLocationIndex;


global using static Raylib_CsLo.ShaderAttributeDataType;
global using static Raylib_CsLo.ShaderLocationIndex;
global using static Raylib_CsLo.ShaderUniformDataType;


global using static Raylib_CsLo.Examples.RLights;
global using static Raylib_CsLo.Examples.RLights.LightType;


global using Camera = Raylib_CsLo.Camera3D;
global using RenderTexture2D = Raylib_CsLo.RenderTexture;
global using Texture2D = Raylib_CsLo.Texture;
global using TextureCubemap = Raylib_CsLo.Texture;

global using Matrix = System.Numerics.Matrix4x4;



//////////////  CORE

Raylib_CsLo.Examples.Core.BasicWindow.main();
Raylib_CsLo.Examples.Core.BasicScreenManager.main();
Raylib_CsLo.Examples.Core.KeyboardInput.main();
Raylib_CsLo.Examples.Core.InputMouse.main();
Raylib_CsLo.Examples.Core.InputMouseWheel.main();
Raylib_CsLo.Examples.Core.GamepadInput.main();
Raylib_CsLo.Examples.Core.InputMultitouch.main();
Raylib_CsLo.Examples.Core.InputGesturesDetection.main();


Raylib_CsLo.Examples.Core.Camera2d.main();
Raylib_CsLo.Examples.Core.Camera2dPlatformer.main();
Raylib_CsLo.Examples.Core.Camera3dMode.main();
Raylib_CsLo.Examples.Core.Camera3dFree.main();
Raylib_CsLo.Examples.Core.Camera3dFirstPerson.main();
Raylib_CsLo.Examples.Core.Picking3d.main();
Raylib_CsLo.Examples.Core.WorldToScreen.main();
Raylib_CsLo.Examples.Core.CustomLogging.main();


Raylib_CsLo.Examples.Core.WindowLetterbox.main();
Raylib_CsLo.Examples.Core.WindowsDropFiles.main();
Raylib_CsLo.Examples.Core.ScissorTest.main();
Raylib_CsLo.Examples.Core.VrSimulator.main();
Raylib_CsLo.Examples.Core.QuatConversions.main();
Raylib_CsLo.Examples.Core.WindowFlags.main();
Raylib_CsLo.Examples.Core.SplitScreen.main();
Raylib_CsLo.Examples.Core.SmoothPixelPerfectCamera.main();

//the following example requires a custom build of raylib to work.  see it's docs for info.
//Raylib_CsLo.Examples.Core.CustomFrameControl.main();


////////////  MODELS

Raylib_CsLo.Examples.Models.Animation.main();
Raylib_CsLo.Examples.Models.Billboard.main();
Raylib_CsLo.Examples.Models.BoxCollisions.main();
Raylib_CsLo.Examples.Models.Cubicmap.main();
Raylib_CsLo.Examples.Models.FirstPersonMaze.main();
Raylib_CsLo.Examples.Models.GeometricShapes.main();
Raylib_CsLo.Examples.Models.Heightmap.main();
Raylib_CsLo.Examples.Models.Loading.main();
Raylib_CsLo.Examples.Models.LoadingGltf.main();
Raylib_CsLo.Examples.Models.LoadingVox.main();
Raylib_CsLo.Examples.Models.MeshGeneration.main();
Raylib_CsLo.Examples.Models.MeshPicking.main();
Raylib_CsLo.Examples.Models.OrthographicProjection.main();
Raylib_CsLo.Examples.Models.RlglSolarSystem.main();
Raylib_CsLo.Examples.Models.Skybox.main();
Raylib_CsLo.Examples.Models.WavingCubes.main();
Raylib_CsLo.Examples.Models.YawPitchRoll.main();


////////// SHADERS

Raylib_CsLo.Examples.Shaders.BasicLighting.main();
Raylib_CsLo.Examples.Shaders.CustomUniform.main();
Raylib_CsLo.Examples.Shaders.Eratosthenes.main();
Raylib_CsLo.Examples.Shaders.Fog.main();
Raylib_CsLo.Examples.Shaders.HotReloading.main();
Raylib_CsLo.Examples.Shaders.JuliaSet.main();
Raylib_CsLo.Examples.Shaders.MeshInstancing.main();
Raylib_CsLo.Examples.Shaders.ModelShader.main();
Raylib_CsLo.Examples.Shaders.MultiSample2d.main();
Raylib_CsLo.Examples.Shaders.PaletteColorSwitch.main();

Raylib_CsLo.Examples.Shaders.PostProcessingShader.main();
Raylib_CsLo.Examples.Shaders.RaymarchingShapes.main();
Raylib_CsLo.Examples.Shaders.ShapesAndTextureShaders.main();
Raylib_CsLo.Examples.Shaders.SimpleMask.main();
Raylib_CsLo.Examples.Shaders.Spotlight.main();
Raylib_CsLo.Examples.Shaders.TextureDrawing.main();
Raylib_CsLo.Examples.Shaders.TextureOutline.main();
Raylib_CsLo.Examples.Shaders.TextureWaves.main();
