"Building projects..."

# change dir to folder containing script
pushd $PSScriptRoot

rm .\raylib-with-extras\ -Recurse

./premake5.exe vs2022
"Windows project built..."

#./premake5.exe gmake2
#"Linux project built..."

#./premake5.exe xcode4