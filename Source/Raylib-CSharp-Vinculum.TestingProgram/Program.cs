
//------------------------------------------------------------------------------
//
// Copyright 2022-2024 (C) Raylib-CSharp-Vinculum, Raylib-CsLo and Contributors. 
// This file is licensed to you under the MPL-2.0.
// See the LICENSE file in the project's root for more info.
//
// Raylib-CSharp-Vinculum, .Net/C# bindings for raylib 5.0.
// Find Raylib-CSharp-Vinculum here: https://github.com/ZeroElectric/Raylib-CSharp-Vinculum
// Find raylib here: https://github.com/raysan5/raylib
//
//------------------------------------------------------------------------------

///////////////////////////////////////////
//////	ALL THE EXAMPLES. You can comment out the ones you don't want to run.

///////////////////////////////////////////
//////////////	CORE

ZeroElectric.Vinculum.ExampleCore.Core.BasicWindow.main();
ZeroElectric.Vinculum.ExampleCore.Core.BasicScreenManager.main();
ZeroElectric.Vinculum.ExampleCore.Core.KeyboardInput.main();
ZeroElectric.Vinculum.ExampleCore.Core.InputMouse.main();
ZeroElectric.Vinculum.ExampleCore.Core.InputMouseWheel.main();
ZeroElectric.Vinculum.ExampleCore.Core.GamepadInput.main();
ZeroElectric.Vinculum.ExampleCore.Core.InputMultitouch.main();
ZeroElectric.Vinculum.ExampleCore.Core.InputGesturesDetection.main();
ZeroElectric.Vinculum.ExampleCore.Core.Camera2d.main();
ZeroElectric.Vinculum.ExampleCore.Core.Camera2DMouseZoom.main();
ZeroElectric.Vinculum.ExampleCore.Core.Camera2dPlatformer.main();
ZeroElectric.Vinculum.ExampleCore.Core.Camera2DSplitScreen.main();
ZeroElectric.Vinculum.ExampleCore.Core.Camera3dMode.main();
ZeroElectric.Vinculum.ExampleCore.Core.Camera3dFree.main();
ZeroElectric.Vinculum.ExampleCore.Core.Camera3dFirstPerson.main();
ZeroElectric.Vinculum.ExampleCore.Core.Picking3d.main();
ZeroElectric.Vinculum.ExampleCore.Core.WorldToScreen.main();

if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
{
	ZeroElectric.Vinculum.ExampleCore.Core.CustomLogging.main(); // This is Windows only!
}

ZeroElectric.Vinculum.ExampleCore.Core.WindowLetterbox.main();
ZeroElectric.Vinculum.ExampleCore.Core.WindowsDropFiles.main();
ZeroElectric.Vinculum.ExampleCore.Core.ScissorTest.main();
ZeroElectric.Vinculum.ExampleCore.Core.VrSimulator.main();
ZeroElectric.Vinculum.ExampleCore.Core.QuatConversions.main();
ZeroElectric.Vinculum.ExampleCore.Core.WindowFlags.main();
ZeroElectric.Vinculum.ExampleCore.Core.SplitScreen.main();
ZeroElectric.Vinculum.ExampleCore.Core.SmoothPixelPerfectCamera.main();

///////////////////////////////////////////
//////////////	OTHER

ZeroElectric.Vinculum.ExampleCore.Other.VectorAngle.main();

////Raylib_CsLo.Examples.Core.CustomFrameControl.main(); // The following example requires a custom build of raylib to work. See raylib docs for it's info.

///////////////////////////////////////////
//////////////	MODELS
ZeroElectric.Vinculum.ExampleCore.Models.Animation.main();
ZeroElectric.Vinculum.ExampleCore.Models.Billboard.main();
ZeroElectric.Vinculum.ExampleCore.Models.BoxCollisions.main();
ZeroElectric.Vinculum.ExampleCore.Models.Cubicmap.main();
ZeroElectric.Vinculum.ExampleCore.Models.FirstPersonMaze.main();
ZeroElectric.Vinculum.ExampleCore.Models.GeometricShapes.main();
ZeroElectric.Vinculum.ExampleCore.Models.Heightmap.main();
ZeroElectric.Vinculum.ExampleCore.Models.Loading.main();
ZeroElectric.Vinculum.ExampleCore.Models.LoadingGLTF.main();
ZeroElectric.Vinculum.ExampleCore.Models.LoadingVox.main();
ZeroElectric.Vinculum.ExampleCore.Models.LoadingM3D.main();
ZeroElectric.Vinculum.ExampleCore.Models.MeshGeneration.main();
ZeroElectric.Vinculum.ExampleCore.Models.MeshPicking.main();
ZeroElectric.Vinculum.ExampleCore.Models.OrthographicProjection.main();
ZeroElectric.Vinculum.ExampleCore.Models.RlglSolarSystem.main();
ZeroElectric.Vinculum.ExampleCore.Models.Skybox.main(); // This example does not work right // TODO (KEN) FIX
ZeroElectric.Vinculum.ExampleCore.Models.WavingCubes.main();
ZeroElectric.Vinculum.ExampleCore.Models.YawPitchRoll.main();
ZeroElectric.Vinculum.ExampleCore.Models.DrawCubeWithTexture.main();

///////////////////////////////////////////
//////////////	SHADERS
ZeroElectric.Vinculum.ExampleCore.Shaders.BasicLighting.main();
ZeroElectric.Vinculum.ExampleCore.Shaders.CustomUniform.main();
ZeroElectric.Vinculum.ExampleCore.Shaders.Eratosthenes.main();
ZeroElectric.Vinculum.ExampleCore.Shaders.Fog.main();
ZeroElectric.Vinculum.ExampleCore.Shaders.HotReloading.main();
ZeroElectric.Vinculum.ExampleCore.Shaders.JuliaSet.main();
ZeroElectric.Vinculum.ExampleCore.Shaders.MeshInstancing.main();
ZeroElectric.Vinculum.ExampleCore.Shaders.ModelShader.main();
ZeroElectric.Vinculum.ExampleCore.Shaders.MultiSample2d.main();
ZeroElectric.Vinculum.ExampleCore.Shaders.PaletteColorSwitch.main();
ZeroElectric.Vinculum.ExampleCore.Shaders.PostProcessingShader.main();
ZeroElectric.Vinculum.ExampleCore.Shaders.RaymarchingShapes.main();
ZeroElectric.Vinculum.ExampleCore.Shaders.ShapesAndTextureShaders.main();
ZeroElectric.Vinculum.ExampleCore.Shaders.SimpleMask.main();
ZeroElectric.Vinculum.ExampleCore.Shaders.Spotlight.main();
ZeroElectric.Vinculum.ExampleCore.Shaders.TextureDrawing.main();
ZeroElectric.Vinculum.ExampleCore.Shaders.TextureOutline.main();
ZeroElectric.Vinculum.ExampleCore.Shaders.TextureWaves.main();

///////////////////////////////////////////
//////////////	TEXTURES
ZeroElectric.Vinculum.ExampleCore.Textures.BackgroundScrolling.main();
ZeroElectric.Vinculum.ExampleCore.Textures.BlendModes.main();
ZeroElectric.Vinculum.ExampleCore.Textures.Bunnymark.main();
ZeroElectric.Vinculum.ExampleCore.Textures.ImageDrawing.main();
ZeroElectric.Vinculum.ExampleCore.Textures.ImageGeneration.main();
ZeroElectric.Vinculum.ExampleCore.Textures.ImageLoading.main();
ZeroElectric.Vinculum.ExampleCore.Textures.ImageRotation.main();
ZeroElectric.Vinculum.ExampleCore.Textures.ImageProcessing.main();
ZeroElectric.Vinculum.ExampleCore.Textures.ImageTextDrawing.main();
ZeroElectric.Vinculum.ExampleCore.Textures.LoadingAndDrawing.main();
ZeroElectric.Vinculum.ExampleCore.Textures.FogOfWar.main();
ZeroElectric.Vinculum.ExampleCore.Textures.SVGLoading.main();
ZeroElectric.Vinculum.ExampleCore.Textures.MousePainting.main();
ZeroElectric.Vinculum.ExampleCore.Textures.TexturedCurve.main();
ZeroElectric.Vinculum.ExampleCore.Textures.NPatchDrawing.main();
ZeroElectric.Vinculum.ExampleCore.Textures.ParticlesBlending.main();
ZeroElectric.Vinculum.ExampleCore.Textures.SpriteButton.main();
ZeroElectric.Vinculum.ExampleCore.Textures.SpriteExplosion.main();
ZeroElectric.Vinculum.ExampleCore.Textures.TexturedPolygon.main();
ZeroElectric.Vinculum.ExampleCore.Textures.TextureFromRawData.main();
ZeroElectric.Vinculum.ExampleCore.Textures.TextureRectangle.main();
ZeroElectric.Vinculum.ExampleCore.Textures.TextureSourceAndDestinationRectangles.main();
ZeroElectric.Vinculum.ExampleCore.Textures.TextureToImage.main();
ZeroElectric.Vinculum.ExampleCore.Textures.TiledExture.main();

///////////////////////////////////////////
//////////////	TEXT
ZeroElectric.Vinculum.ExampleCore.Text.Draw2dIn3d.main();
ZeroElectric.Vinculum.ExampleCore.Text.DrawTextInsideRectangle.main();
ZeroElectric.Vinculum.ExampleCore.Text.FontFilters.main();
ZeroElectric.Vinculum.ExampleCore.Text.FontLoading.main();
ZeroElectric.Vinculum.ExampleCore.Text.FontLoadingUsage.main();
ZeroElectric.Vinculum.ExampleCore.Text.InputBox.main();
ZeroElectric.Vinculum.ExampleCore.Text.SdfFonts.main();
ZeroElectric.Vinculum.ExampleCore.Text.SpriteFontLoading.main();
ZeroElectric.Vinculum.ExampleCore.Text.TextFormatting.main();
ZeroElectric.Vinculum.ExampleCore.Text.WritingAnimation.main();

///////////////////////////////////////////
//////////////	SHAPES
ZeroElectric.Vinculum.ExampleCore.Shapes.BasicShapesDrawing.main();
ZeroElectric.Vinculum.ExampleCore.Shapes.BoundingBall.main();
ZeroElectric.Vinculum.ExampleCore.Shapes.CollisionArea.main();
ZeroElectric.Vinculum.ExampleCore.Shapes.ColorsPalette.main();
ZeroElectric.Vinculum.ExampleCore.Shapes.DrawCircleSector.main();
ZeroElectric.Vinculum.ExampleCore.Shapes.DrawRectangleRounded.main();
ZeroElectric.Vinculum.ExampleCore.Shapes.DrawRing.main();
ZeroElectric.Vinculum.ExampleCore.Shapes.EasingsBallAnim.main();
ZeroElectric.Vinculum.ExampleCore.Shapes.EasingsBoxAnim.main();
ZeroElectric.Vinculum.ExampleCore.Shapes.EasingsRectangleArray.main();
ZeroElectric.Vinculum.ExampleCore.Shapes.FollowingEyes.main();
ZeroElectric.Vinculum.ExampleCore.Shapes.LogoAnimation.main();
ZeroElectric.Vinculum.ExampleCore.Shapes.LogoUsingShapes.main();
ZeroElectric.Vinculum.ExampleCore.Shapes.RectangleScalingMouse.main();
ZeroElectric.Vinculum.ExampleCore.Shapes.LinesCubicBezier.main();
ZeroElectric.Vinculum.ExampleCore.Shapes.SplinesDrawing.main();


///////////////////////////////////////////
//////////////	PHYSICS
ZeroElectric.Vinculum.ExampleCore.Physics.PhysicsDemo.main();
ZeroElectric.Vinculum.ExampleCore.Physics.PhysicsFriction.main();
ZeroElectric.Vinculum.ExampleCore.Physics.PhysicsMovement.main();
ZeroElectric.Vinculum.ExampleCore.Physics.PhysicsRestitution.main();
ZeroElectric.Vinculum.ExampleCore.Physics.PhysicsShatter.main();

/////////////////////////////////////////
////////////	AUDIO
ZeroElectric.Vinculum.ExampleCore.Audio.MixedAudioProcessing.main();
ZeroElectric.Vinculum.ExampleCore.Audio.RawAudioStreaming.main();
ZeroElectric.Vinculum.ExampleCore.Audio.SoundLoadingAndPlaying.main();
ZeroElectric.Vinculum.ExampleCore.Audio.MusicPlayingStreaming.main();
ZeroElectric.Vinculum.ExampleCore.Audio.ModulePlayingStreaming.main();
