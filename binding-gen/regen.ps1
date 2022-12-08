
pushd $PSScriptRoot

del -Recurse -Force ..\Raylib-CSharp-Vinculum\autogen
del -Recurse -Force ..\Raylib-CSharp-Vinculum.Tests\autogen

#pushd ..\sub-modules\ClangSharp\
#dotnet build -c Release
#popd

$RaylibSrc = "../sub-modules/raylib/src"
$RaylibExtrasSrc = "../sub-modules/raylib/src/extras"
$PhysacSrc = "../sub-modules/physac/src"
$RayGuiSrc = "../sub-modules/raygui/src"
$RResSrc = "../sub-modules/rres/src"
$EasingsSrc = "../sub-modules/raylib/examples/others"

"### Building: raylib"
dotnet ..\sub-modules\ClangSharp\ClangSharpPInvokeGenerator\Release\net6.0\ClangSharpPInvokeGenerator.dll @gen-raylib.rsp --file-directory "$RaylibSrc" --file raylib.h --methodClassName Raylib --libraryPath raylib --exclude PI DEG2RAD RAD2DEG
"### Building: raymath"
dotnet ..\sub-modules\ClangSharp\ClangSharpPInvokeGenerator\Release\net6.0\ClangSharpPInvokeGenerator.dll @gen-raylib.rsp --file-directory "$RaylibSrc" --file raymath.h --methodClassName RayMath --libraryPath raylib
"### Building: rlgl"
dotnet ..\sub-modules\ClangSharp\ClangSharpPInvokeGenerator\Release\net6.0\ClangSharpPInvokeGenerator.dll @gen-raylib.rsp --file-directory "$RaylibSrc" --file rlgl.h --methodClassName RlGl --libraryPath raylib
"### Building: raygui"
dotnet ..\sub-modules\ClangSharp\ClangSharpPInvokeGenerator\Release\net6.0\ClangSharpPInvokeGenerator.dll @gen-raylib.rsp --file-directory "$RayGuiSrc" --file raygui.h --methodClassName RayGui --libraryPath raylib --include-directory "$RaylibSrc"
"### Building: physac"
dotnet ..\sub-modules\ClangSharp\ClangSharpPInvokeGenerator\Release\net6.0\ClangSharpPInvokeGenerator.dll @gen-raylib.rsp --file-directory "$PhysacSrc" --include-directory "$RaylibSrc" --file physac.h --methodClassName Physac --libraryPath raylib
"### Building: rres"
dotnet ..\sub-modules\ClangSharp\ClangSharpPInvokeGenerator\Release\net6.0\ClangSharpPInvokeGenerator.dll @gen-raylib.rsp --file-directory "$RResSrc" --file rres.h --methodClassName RRes --libraryPath raylib --include-directory "$RaylibSrc"
"### Building: Easings "
dotnet ..\sub-modules\ClangSharp\ClangSharpPInvokeGenerator\Release\net6.0\ClangSharpPInvokeGenerator.dll @gen-raylib.rsp --file-directory "$EasingsSrc" --include-directory "$RaylibSrc" --file reasings.h --methodClassName Easings --exclude EaseElasticInOut PI DEG2RAD RAD2DEG

"### FIXING FILES..."

$path = "../Raylib-CSharp-Vinculum/autogen/bindings/"
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

popd

"### DONE!"