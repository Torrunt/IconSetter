cd "C:\Program Files (x86)\Microsoft\ILMerge"

mkdir "%~dp0IconSetter\bin\IconSetter\"

ILMerge.exe /target:winexe /targetplatform:"v4,C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.5" /out:"%~dp0IconSetter\bin\IconSetter\IconSetter.exe" "%~dp0IconSetter\bin\Release\IconSetter.exe" "%~dp0IconSetter\ObjectListView.dll"

del "%~dp0IconSetter\bin\IconSetter\IconSetter.pdb"
