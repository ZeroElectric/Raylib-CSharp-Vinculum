## 4.5.0.1 (Apr 7 2023): 
- Added Linux support.

## 4.5.0 (Mar 18 2023),
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
