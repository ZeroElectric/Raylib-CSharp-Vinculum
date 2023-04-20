workspace "Raylib-With-Extras"
	configurations { "Debug", "Release" }
	platforms { "x64"}

	filter "configurations:Debug"
		defines { "DEBUG" }
		symbols "On"
		
	filter "configurations:Release"
		defines { "NDEBUG" }
		optimize "On"	
				
	filter { "platforms:x64" }
		architecture "x86_64"
			
	defines{"PLATFORM_DESKTOP", "GRAPHICS_API_OPENGL_33"} -- "GRAPHICS_API_OPENGL_43"
	
--The raylib-with-extras project, but named "raylib" so the output libraries are named properly
project "raylib"
	filter "configurations:Debug OR Release"
		kind "SharedLib" -- "StaticLib"
		defines {"BUILD_LIBTYPE_SHARED"}

	filter "action:vs*"
		defines{"_WINSOCK_DEPRECATED_NO_WARNINGS", "_CRT_SECURE_NO_WARNINGS", "_WIN32"}
		targetdir "../Source/Raylib-CSharp-Vinculum/runtimes/win-x64/native"
		links {"winmm"}
		
	filter "action:gmake*"
		targetdir "../Source/Raylib-CSharp-Vinculum/runtimes/linux-x64/native"
		links {"pthread", "GL", "m", "dl", "rt", "X11"}
		
	filter{}
	
	location "raylib-with-extras"
	language "C++"
	cppdialect "C++17"
	
	includedirs { "../Libs/raylib/src","../Libs/raylib/src/external/glfw/include", 	
	"../Libs/raylib-extra-shims/src", --extras .c files	
	"../Libs/physac/src/","../Libs/raygui/src/","../Libs/rres/src/" --extras .h files
	} 
	vpaths 
	{
		["Header Files"] = { "../Libs/raylib/src/**.h"},
		["Source Files/*"] = {"../Libs/raylib/src/**.c"},
		["Extras Headers"] = { "../Libs/physac/src/**.h","../Libs/raygui/src/**.h","../Libs/rres/src/**.h"},
		["Extras Sources"] = { "raylib-extra-shims/src/**.c"},
	}
	files {
		"../Libs/raylib/src/*.h", 
		"../Libs/raylib/src/*.c",
		"../Libs/physac/src/**.h",
		"../Libs/raygui/src/**.h",
		"../Libs/rres/src/**.h", 
		"raylib-extra-shims/src/*.c"
	}