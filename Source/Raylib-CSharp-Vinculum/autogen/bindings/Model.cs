
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

//------------------------------------------------------------------------------
//
//     This file and it's containing folder are generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
//
//------------------------------------------------------------------------------

using System.Numerics;

namespace ZeroElectric.Vinculum
{
    public unsafe partial struct Model
    {
        [NativeTypeName("Matrix")]
        public Matrix4x4 transform;

        public int meshCount;

        public int materialCount;

        public Mesh* meshes;

        public Material* materials;

        public int* meshMaterial;

        public int boneCount;

        public BoneInfo* bones;

        public Transform* bindPose;
    }
}
