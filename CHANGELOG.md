# 5.0 (Nov 22 2023), Raylib 5.0 | RayGUI 4.0 
 
### Raylib & RayGUI Changes :

- Raylib 5.0
    - [BRAKING] REMOVED: GenImageGradientH() and GenImageGradientV()
    - [BRAKING] RENAMED: LoadFont*() parameter names for consistenc
    - ADDED: New Splines drawing and evaluation API
    - ADDED: IsKeyPressedRepeat() on PLATFORM_DESKTOP
    - ADDED: SetWindowMaxSize() for desktop and web
    - ADDED: LoadRandomSequence() / UnloadRandomSequence()
    - ADDED: DrawCircleLinesV()
    - ADDED: ExportImageToMemory()
    - ADDED: GenImageGradientSquare()
    - ADDED: GenImageLinearGradient()
    - ADDED: LoadSoundAlias()
    - ADDED: GetMasterVolume()
    - REDESIGNED: LoadOBJ(), to avoid mesh splitting by materials
    - REVIEWED: Support .vox model file version 200 
    - REVIEWED: Optimized and simplified  the gesture system 
    - And much much more

#### With over 95 function changed & additions be sure to read up on all of the changes over on raylib's' [CHANGELOG]([CHANGELOG](https://github.com/raysan5/raylib/blob/master/CHANGELOG))!

- RayGUI 4.0
    - [BRAKING] REDESIGNED: Multiple functions and what they return, check out raygui 4.0's [CHANGELOG]([CHANGELOG](https://github.com/raysan5/raygui/releases/tag/4.0)) for a detailed list of changes
    - [BRAKING] REDESIGNED: All controls return result are now 'int'
    - ADDED: GuiToggleSlider()
    - ADDED: GuiColorPickerHSV() and GuiColorPanelHSV()
    - ADDED: Multiple new icons, mostly compiler related
    - ADDED: New enum values: GuiTextAlignment, GuiTextAlignmentVertical, GuiTextWrapMode


### Vinculum Changes
- Added two new examples
    - SplinesDrawing
    - MixedAudioProcessing
- Created a Attributions file for example assets
- Optimized the layout of multiple of the examples
- Optimized port of rcamera, #6 
- Fixed .obj example files being missing
- Fixed a crash on Linux with the TestingProgram
- Fixed all examples with freecamera to behave properly

#### Known Issues:

- GuiCheckBox wrapper implementation does not have correct API, returns bool like the raygui 3.x API and not int.
- Many of the new API's do not have a managed sbyte* wrapper implementations.

# 4.5.1 (Apr 20 2023), Raygui 3.5 

### Updated raygui to 3.5 
- Added new Tab Bar contorol `GuiTabBar()`, based upon `GuiToggle()`,
- Added helper functions to split text in separate lines,
- Added new icons useful for code editing tools,
- Redesigned `GuiTextBox()` to support cursor movement and `GuiDrawText()` to divide drawing by lines.

#### The following functions have been removed:
- REMOVED: Unneeded icon editing functions
- `REMOVED: GuiTextBoxMulti()`, very limited and broken
- REMOVED: MeasureTextEx() dependency, logic directly implemented
- REMOVED: DrawTextEx() dependency, logic directly implemented

# 4.5.0.1 (Apr 7 2023), Linux support
- Added Linux support.

# 4.5.0 (Mar 18 2023), Raylib 4.5

### Updated raylib to 4.5 

- A simpler and more extendable Camera module (RayCamera a C# port of rcamera.h).
- Support for M3D models and M3D/GLTF animations, 
- Support for QOA audio format, 
- Added new data structures validation functions such as `IsImageReady()`, `IsTextureReady()`, `IsSoundReady()` & more!
- Redesigned rlgl module for automatic render-batch limits checking and rshapes module to minimize the rlgl dependency.

#### The following functions have been removed or renamed:

- REMOVED: Multichannel audio API: PlaySoundMulti(), StopSoundMulti()
- REMOVED: UnloadModelKeepMeshes()
- REMOVED: DrawCubeTexture(), DrawCubeTextureRec(), functions moved to new example: `DrawCubeWithTexture`

- REMOVED: DrawTextureQud()
- REMOVED: DrawTexturePoly(), function moved to example: `textures_polygon`
- REMOVED: DrawTextureTiled(),function implementation moved to the textures_tiled.c

- RENAMED: TextCodepointsToUTF8() to LoadUTF8()
- RENAMED: GetCodepoint() -> GetCodepointNext()

#### With more then 25 new functions and 40+ function revsions make sure to check out raylib's [CHANGELOG](https://github.com/raysan5/raylib/blob/master/CHANGELOG) for a detailed list of changes in raylib 4.5


# 4.2.0 (Dec 8, 2022), Initial release
- Initial release of Raylib-CSharp-Vinculum, a C# binding for Raylib 4.2, supports Windows.
- Fork of [Raylib-CsLo](https://github.com/NotNotTech/Raylib-CsLo)