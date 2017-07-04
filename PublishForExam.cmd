@ECHO OFF
SET MSBuildExe=MSBuild.exe
SET Project=Publish.proj
SET EnableTrace=False
SET PublishProfile=DEV
SET VisualStudioVersion=11.0

"%MSBuildExe%" "%Project%" /p:EnableTrace=%EnableTrace%;PublishProfile=%PublishProfile%;VisualStudioVersion=%VisualStudioVersion% /fl