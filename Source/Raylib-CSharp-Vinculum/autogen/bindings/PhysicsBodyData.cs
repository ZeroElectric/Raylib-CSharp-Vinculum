
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
using ZeroElectric.Vinculum.Extensions;

namespace ZeroElectric.Vinculum
{
    public partial struct PhysicsBodyData
    {
        [NativeTypeName("unsigned int")]
        public uint id;

        public Bool enabled;

        public Vector2 position;

        public Vector2 velocity;

        public Vector2 force;

        public float angularVelocity;

        public float torque;

        public float orient;

        public float inertia;

        public float inverseInertia;

        public float mass;

        public float inverseMass;

        public float staticFriction;

        public float dynamicFriction;

        public float restitution;

        public Bool useGravity;

        public Bool isGrounded;

        public Bool freezeOrient;

        public PhysicsShape shape;
    }
}
