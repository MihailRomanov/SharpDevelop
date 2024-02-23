@for /f "usebackq tokens=*" %%i in (`src\Tools\VSWhere\vswhere.exe -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe`) do (
  set msbuild=%%i
)

"%msbuild%" src\Automated.proj -t:init
