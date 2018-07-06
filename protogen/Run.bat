   @echo off

::协议文件路径, 最后不要跟“\”符号

set PATH_ROOT=E:\XueXi\Unity_Net_Socket\UnitySocket\protogen

set SOURCE_FOLDER=%PATH_ROOT%\proto

::C#编译器路径
set CS_COMPILER_PATH=%PATH_ROOT%\protogen.exe
::C#文件生成路径, 最后不要跟“\”符号
set CS_TARGET_PATH=E:\XueXi\Unity_Net_Socket\UnitySocket\Assets\Script\Proto_cs

 
::删除之前创建的文件
del %CS_TARGET_PATH%\*.* /f /s /q
 
E:
cd %SOURCE_FOLDER%
::遍历所有文件
for /R %%i in (*.proto) do (
    
    ::生成 C# 代码
    echo 生成 %%~ni.cs
    %CS_COMPILER_PATH% -i:%%i -o:%CS_TARGET_PATH%\%%~ni.cs
    
)

echo 协议生成完毕。

pause