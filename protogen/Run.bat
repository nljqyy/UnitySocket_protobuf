   @echo off

::Э���ļ�·��, ���Ҫ����\������

set PATH_ROOT=E:\XueXi\Unity_Net_Socket\UnitySocket\protogen

set SOURCE_FOLDER=%PATH_ROOT%\proto

::C#������·��
set CS_COMPILER_PATH=%PATH_ROOT%\protogen.exe
::C#�ļ�����·��, ���Ҫ����\������
set CS_TARGET_PATH=E:\XueXi\Unity_Net_Socket\UnitySocket\Assets\Script\Proto_cs

 
::ɾ��֮ǰ�������ļ�
del %CS_TARGET_PATH%\*.* /f /s /q
 
E:
cd %SOURCE_FOLDER%
::���������ļ�
for /R %%i in (*.proto) do (
    
    ::���� C# ����
    echo ���� %%~ni.cs
    %CS_COMPILER_PATH% -i:%%i -o:%CS_TARGET_PATH%\%%~ni.cs
    
)

echo Э��������ϡ�

pause