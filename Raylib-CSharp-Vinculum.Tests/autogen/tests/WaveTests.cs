
//------------------------------------------------------------------------------
//
// Copyright 2022 © Raylib-CSharp-Vinculum, Raylib-CsLo and Contributors. 
// This file is licensed to you under the MPL-2.0.
// See the LICENSE file in the project's root for more info.
//
// Raylib-CSharp-Vinculum, bindings for Raylib 4.2.
// Find Raylib-CSharp-Vinculum here: https://github.com/ZeroElectric/Raylib-CSharp-Vinculum
// Find Raylib here: https://github.com/raysan5/raylib
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

using System;
using System.Runtime.InteropServices;
using Xunit;

namespace ZeroElectric.Vinculum.UnitTests
{
    /// <summary>Provides validation of the <see cref="Wave" /> struct.</summary>
    public static unsafe partial class WaveTests
    {
        /// <summary>Validates that the <see cref="Wave" /> struct is blittable.</summary>
        [Fact]
        public static void IsBlittableTest()
        {
            Assert.Equal(sizeof(Wave), Marshal.SizeOf<Wave>());
        }

        /// <summary>Validates that the <see cref="Wave" /> struct has the right <see cref="LayoutKind" />.</summary>
        [Fact]
        public static void IsLayoutSequentialTest()
        {
            Assert.True(typeof(Wave).IsLayoutSequential);
        }

        /// <summary>Validates that the <see cref="Wave" /> struct has the correct size.</summary>
        [Fact]
        public static void SizeOfTest()
        {
            if (Environment.Is64BitProcess)
            {
                Assert.Equal(24, sizeof(Wave));
            }
            else
            {
                Assert.Equal(20, sizeof(Wave));
            }
        }
    }
}