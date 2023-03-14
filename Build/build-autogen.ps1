
Push-Location $PSScriptRoot

Remove-Item -Recurse -Force ..\Source\Raylib-CSharp-Vinculum\autogen
Remove-Item -Recurse -Force ..\Source\Raylib-CSharp-Vinculum.Tests\autogen

Copy-Item -Force -path "..\Source\Raylib-CSharp-Vinculum\runtimes\win-x64\native\raylib.dll" -Destination "..\Source\Raylib-CSharp-Vinculum"
Copy-Item -Force -path "..\Source\Raylib-CSharp-Vinculum\runtimes\win-x64\native\raylib.pdb" -Destination "..\Source\Raylib-CSharp-Vinculum"

$RaylibSrc = "../Libs/raylib/src"
$PhysacSrc = "../Libs/physac/src"
$RayGuiSrc = "../Libs/raygui/src"
$RResSrc = "../Libs/rres/src"
$EasingsSrc = "../Libs/raylib/examples/others"

"### Building: raylib"
dotnet ClangSharp\ClangSharpPInvokeGenerator.dll @gen-raylib.rsp --file-directory "$RaylibSrc" --file raylib.h --methodClassName Raylib --libraryPath raylib --exclude PI DEG2RAD RAD2DEG
"### Building: raymath"
dotnet ClangSharp\ClangSharpPInvokeGenerator.dll @gen-raylib.rsp --file-directory "$RaylibSrc" --file raymath.h --methodClassName RayMath --libraryPath raylib
"### Building: rlgl"
dotnet ClangSharp\ClangSharpPInvokeGenerator.dll @gen-raylib.rsp --file-directory "$RaylibSrc" --file rlgl.h --methodClassName RlGl --libraryPath raylib
#"### Building: raycamera"
#dotnet ClangSharp\ClangSharpPInvokeGenerator.dll @gen-raylib.rsp --file-directory "$RaylibSrc" --file rcamera.h --methodClassName RayCamera --libraryPath raylib
"### Building: raygui"
dotnet ClangSharp\ClangSharpPInvokeGenerator.dll @gen-raylib.rsp --file-directory "$RayGuiSrc" --file raygui.h --methodClassName RayGui --libraryPath raylib --include-directory "$RaylibSrc"
"### Building: physac"
dotnet ClangSharp\ClangSharpPInvokeGenerator.dll @gen-raylib.rsp --file-directory "$PhysacSrc" --include-directory "$RaylibSrc" --file physac.h --methodClassName Physac --libraryPath raylib
"### Building: rres"
dotnet ClangSharp\ClangSharpPInvokeGenerator.dll @gen-raylib.rsp --file-directory "$RResSrc" --file rres.h --methodClassName RRes --libraryPath raylib --include-directory "$RaylibSrc"
"### Building: reasings "
dotnet ClangSharp\ClangSharpPInvokeGenerator.dll @gen-raylib.rsp --file-directory "$EasingsSrc" --include-directory "$RaylibSrc" --file reasings.h --methodClassName Easings --exclude EaseElasticInOut PI DEG2RAD RAD2DEG

"### FIXING FILES..."

$path = "../Source/Raylib-CSharp-Vinculum/autogen/bindings/"
foreach ($file in Get-ChildItem $path) {
	$target = $path + $file
	Write-Output "=======  PROCESSING:        $target"
	##hack: replace malformed autogen content
	$tempContents = (Get-Content $target -Raw).replace('.operator=', '=')	
	# make all C bools marshal properly.   see: https://stackoverflow.com/a/4621621
	$tempContents = $tempContents.replace('public static extern Bool ', "[return: MarshalAs(UnmanagedType.U1)] public static extern bool ")
	$tempContents = $tempContents.replace(', Bool ', ", [MarshalAs(UnmanagedType.U1)] bool ")
	$tempContents = $tempContents.replace('(Bool ', "([MarshalAs(UnmanagedType.U1)] bool ")
	#write the file	
	$tempContents | Out-File -FilePath $target -NoNewline
}

Pop-Location

"### DONE!"