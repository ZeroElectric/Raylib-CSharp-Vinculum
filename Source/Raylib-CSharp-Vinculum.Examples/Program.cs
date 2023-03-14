
//------------------------------------------------------------------------------
//
// Copyright 2022-2023 © Raylib-CSharp-Vinculum, Raylib-CsLo and Contributors. 
// This file is licensed to you under the MPL-2.0.
// See the LICENSE file in the project's root for more info.
//
// Raylib-CSharp-Vinculum, bindings for Raylib 4.5.
// Find Raylib-CSharp-Vinculum here: https://github.com/ZeroElectric/Raylib-CSharp-Vinculum
// Find Raylib here: https://github.com/raysan5/raylib
//
//------------------------------------------------------------------------------

/////////////////////////////////////////
// All these `global using` statements are for convience in porting the examples,
// to allow the examples to be copy-pasted mostly verbatum.
// If these statements were not here,
// all the examples would need to be modified: to prefix everything with the proper Class/Enum.

global using System.Numerics;
global using ZeroElectric.Vinculum.InternalHelpers;

global using static ZeroElectric.Vinculum.RayGui;
global using static ZeroElectric.Vinculum.Raylib;
global using static ZeroElectric.Vinculum.RayMath;
global using static ZeroElectric.Vinculum.Easings;
global using static ZeroElectric.Vinculum.RlGl;
global using static ZeroElectric.Vinculum.Physac;

global using static ZeroElectric.Vinculum.Examples.RLights;
global using static ZeroElectric.Vinculum.Examples.RLights.LightType;

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


/////////////////////////////////////////
// The C examples use type aliases, so the following `global using` lines are to match those.
global using Matrix = System.Numerics.Matrix4x4;
global using Camera = ZeroElectric.Vinculum.Camera3D;
global using Texture2D = ZeroElectric.Vinculum.Texture;
global using TextureCubemap = ZeroElectric.Vinculum.Texture;
global using RenderTexture2D = ZeroElectric.Vinculum.RenderTexture;

public static class Program
{
	public static void Main(params string[] args)
	{
		/////////////////////////////////////////
		////////////	MANUAL TEST CASES:  these are test cases for bugfixes to CsLo
		ZeroElectric.Vinculum.Examples.TestCases.NullString.LoadShaderFromMemory.Program.main(args);

		/////////////////////////////////////////
		////////////	ALL THE EXAMPLES. You can comment out the ones you don't want to run.

		/////////////////////////////////////////
		////////////	CORE
		ZeroElectric.Vinculum.Examples.Core.BasicWindow.main();
		ZeroElectric.Vinculum.Examples.Core.BasicScreenManager.main();
		ZeroElectric.Vinculum.Examples.Core.KeyboardInput.main();
		ZeroElectric.Vinculum.Examples.Core.InputMouse.main();
		ZeroElectric.Vinculum.Examples.Core.InputMouseWheel.main();
		ZeroElectric.Vinculum.Examples.Core.GamepadInput.main();
		ZeroElectric.Vinculum.Examples.Core.InputMultitouch.main();
		ZeroElectric.Vinculum.Examples.Core.InputGesturesDetection.main();
		ZeroElectric.Vinculum.Examples.Core.Camera2d.main();
		ZeroElectric.Vinculum.Examples.Core.Camera2dPlatformer.main();
		ZeroElectric.Vinculum.Examples.Core.Camera3dMode.main();
		ZeroElectric.Vinculum.Examples.Core.Camera3dFree.main();
		ZeroElectric.Vinculum.Examples.Core.Camera3dFirstPerson.main();
		ZeroElectric.Vinculum.Examples.Core.Picking3d.main();
		ZeroElectric.Vinculum.Examples.Core.WorldToScreen.main();
		ZeroElectric.Vinculum.Examples.Core.CustomLogging.main();
		ZeroElectric.Vinculum.Examples.Core.WindowLetterbox.main();
		ZeroElectric.Vinculum.Examples.Core.WindowsDropFiles.main();
		ZeroElectric.Vinculum.Examples.Core.ScissorTest.main();
		ZeroElectric.Vinculum.Examples.Core.VrSimulator.main();
		ZeroElectric.Vinculum.Examples.Core.QuatConversions.main();
		ZeroElectric.Vinculum.Examples.Core.WindowFlags.main();
		ZeroElectric.Vinculum.Examples.Core.SplitScreen.main();
		ZeroElectric.Vinculum.Examples.Core.SmoothPixelPerfectCamera.main();
		//Raylib_CsLo.Examples.Core.CustomFrameControl.main(); //The following example requires a custom build of Raylib to work. See it's docs for info.

		/////////////////////////////////////////
		////////////	MODELS
		ZeroElectric.Vinculum.Examples.Models.Animation.main();
		ZeroElectric.Vinculum.Examples.Models.Billboard.main();
		ZeroElectric.Vinculum.Examples.Models.BoxCollisions.main();
		ZeroElectric.Vinculum.Examples.Models.Cubicmap.main();
		ZeroElectric.Vinculum.Examples.Models.FirstPersonMaze.main();
		ZeroElectric.Vinculum.Examples.Models.GeometricShapes.main();
		ZeroElectric.Vinculum.Examples.Models.Heightmap.main();
		ZeroElectric.Vinculum.Examples.Models.Loading.main();
		ZeroElectric.Vinculum.Examples.Models.LoadingGltf.main();
		ZeroElectric.Vinculum.Examples.Models.LoadingVox.main();
		ZeroElectric.Vinculum.Examples.Models.MeshGeneration.main();
		ZeroElectric.Vinculum.Examples.Models.MeshPicking.main();
		ZeroElectric.Vinculum.Examples.Models.OrthographicProjection.main();
		ZeroElectric.Vinculum.Examples.Models.RlglSolarSystem.main();
		ZeroElectric.Vinculum.Examples.Models.Skybox.main();
		ZeroElectric.Vinculum.Examples.Models.WavingCubes.main();
		ZeroElectric.Vinculum.Examples.Models.YawPitchRoll.main();
		ZeroElectric.Vinculum.Examples.Models.DrawCubeWithTexture.main();

		/////////////////////////////////////////
		////////////	SHADERS
		ZeroElectric.Vinculum.Examples.Shaders.BasicLighting.main();
		ZeroElectric.Vinculum.Examples.Shaders.CustomUniform.main();
		ZeroElectric.Vinculum.Examples.Shaders.Eratosthenes.main();
		ZeroElectric.Vinculum.Examples.Shaders.Fog.main();
		ZeroElectric.Vinculum.Examples.Shaders.HotReloading.main();
		ZeroElectric.Vinculum.Examples.Shaders.JuliaSet.main();
		ZeroElectric.Vinculum.Examples.Shaders.MeshInstancing.main();
		ZeroElectric.Vinculum.Examples.Shaders.ModelShader.main();
		ZeroElectric.Vinculum.Examples.Shaders.MultiSample2d.main();
		ZeroElectric.Vinculum.Examples.Shaders.PaletteColorSwitch.main();
		ZeroElectric.Vinculum.Examples.Shaders.PostProcessingShader.main();
		ZeroElectric.Vinculum.Examples.Shaders.RaymarchingShapes.main();
		ZeroElectric.Vinculum.Examples.Shaders.ShapesAndTextureShaders.main();
		ZeroElectric.Vinculum.Examples.Shaders.SimpleMask.main();
		ZeroElectric.Vinculum.Examples.Shaders.Spotlight.main();
		ZeroElectric.Vinculum.Examples.Shaders.TextureDrawing.main();
		ZeroElectric.Vinculum.Examples.Shaders.TextureOutline.main();
		ZeroElectric.Vinculum.Examples.Shaders.TextureWaves.main();

		/////////////////////////////////////////
		////////////	TEXTURES
		ZeroElectric.Vinculum.Examples.Textures.BackgroundScrolling.main();
		ZeroElectric.Vinculum.Examples.Textures.BlendModes.main();
		ZeroElectric.Vinculum.Examples.Textures.Bunnymark.main();
		ZeroElectric.Vinculum.Examples.Textures.ImageDrawing.main();
		ZeroElectric.Vinculum.Examples.Textures.ImageGeneration.main();
		ZeroElectric.Vinculum.Examples.Textures.ImageLoading.main();
		ZeroElectric.Vinculum.Examples.Textures.ImageProcessing.main();
		ZeroElectric.Vinculum.Examples.Textures.ImageTextDrawing.main();
		ZeroElectric.Vinculum.Examples.Textures.LoadingAndDrawing.main();
		ZeroElectric.Vinculum.Examples.Textures.MousePainting.main();
		ZeroElectric.Vinculum.Examples.Textures.NPatchDrawing.main();
		ZeroElectric.Vinculum.Examples.Textures.ParticlesBlending.main();
		ZeroElectric.Vinculum.Examples.Textures.SpriteButton.main();
		ZeroElectric.Vinculum.Examples.Textures.SpriteExplosion.main();
		ZeroElectric.Vinculum.Examples.Textures.TexturedPolygon.main();
		ZeroElectric.Vinculum.Examples.Textures.TextureFromRawData.main();
		ZeroElectric.Vinculum.Examples.Textures.TextureRectangle.main();
		ZeroElectric.Vinculum.Examples.Textures.TextureSourceAndDestinationRectangles.main();
		ZeroElectric.Vinculum.Examples.Textures.TextureToImage.main();
		ZeroElectric.Vinculum.Examples.Textures.TiledExture.main();

		/////////////////////////////////////////
		////////////	TEXT
		ZeroElectric.Vinculum.Examples.Text.Draw2dIn3d.main();
		ZeroElectric.Vinculum.Examples.Text.DrawTextInsideRectangle.main();
		ZeroElectric.Vinculum.Examples.Text.FontFilters.main();
		ZeroElectric.Vinculum.Examples.Text.FontLoading.main();
		ZeroElectric.Vinculum.Examples.Text.FontLoadingUsage.main();
		ZeroElectric.Vinculum.Examples.Text.InputBox.main();
		ZeroElectric.Vinculum.Examples.Text.SdfFonts.main();
		ZeroElectric.Vinculum.Examples.Text.SpriteFontLoading.main();
		ZeroElectric.Vinculum.Examples.Text.TextFormatting.main();
		ZeroElectric.Vinculum.Examples.Text.WritingAnimation.main();
		ZeroElectric.Vinculum.Examples.Text.Unicode.main(); // The unicode example doesn't work properly. I guess the fonts don't include the needed unicode chars? //TODO (KEN) Fix this

		/////////////////////////////////////////
		////////////	SHAPES
		ZeroElectric.Vinculum.Examples.Shapes.BasicShapesDrawing.main();
		ZeroElectric.Vinculum.Examples.Shapes.BoundingBall.main();
		ZeroElectric.Vinculum.Examples.Shapes.CollisionArea.main();
		ZeroElectric.Vinculum.Examples.Shapes.ColorsPalette.main();
		ZeroElectric.Vinculum.Examples.Shapes.DrawCircleSector.main();
		ZeroElectric.Vinculum.Examples.Shapes.DrawRectangleRounded.main();
		ZeroElectric.Vinculum.Examples.Shapes.DrawRing.main();
		ZeroElectric.Vinculum.Examples.Shapes.EasingsBallAnim.main();
		ZeroElectric.Vinculum.Examples.Shapes.EasingsBoxAnim.main();
		ZeroElectric.Vinculum.Examples.Shapes.EasingsRectangleArray.main();
		ZeroElectric.Vinculum.Examples.Shapes.FollowingEyes.main();
		ZeroElectric.Vinculum.Examples.Shapes.LogoAnimation.main();
		ZeroElectric.Vinculum.Examples.Shapes.LogoUsingShapes.main();
		ZeroElectric.Vinculum.Examples.Shapes.RectangleScalingMouse.main();
		ZeroElectric.Vinculum.Examples.Shapes.LinesCubicBezier.main();

		/////////////////////////////////////////
		////////////	PHYSICS
		ZeroElectric.Vinculum.Examples.Physics.PhysicsDemo.main();
		ZeroElectric.Vinculum.Examples.Physics.PhysicsFriction.main();
		ZeroElectric.Vinculum.Examples.Physics.PhysicsMovement.main();
		ZeroElectric.Vinculum.Examples.Physics.PhysicsRestitution.main();
		ZeroElectric.Vinculum.Examples.Physics.PhysicsShatter.main();

		/////////////////////////////////////////
		////////////	AUDIO
		ZeroElectric.Vinculum.Examples.Audio.RawAudioStreaming.main();
		ZeroElectric.Vinculum.Examples.Audio.SoundLoadingAndPlaying.main();
		ZeroElectric.Vinculum.Examples.Audio.MusicPlayingStreaming.main();
		ZeroElectric.Vinculum.Examples.Audio.ModulePlayingStreaming.main();
	}
}
