"Building raylib..."

# change dir to folder containing script
pushd $PSScriptRoot

rm .\raylib-with-extras\ -Recurse

./premake5.exe vs2022

#./premake5.exe gmake2
#./premake5.exe xcode4