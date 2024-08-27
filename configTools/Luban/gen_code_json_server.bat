@echo off
set "current_dir=%~dp0"


set "parent_dir=%current_dir:~0,-1%"
cd %parent_dir%
set "parent_dir=%cd%"


set "parent_dir2=%parent_dir%\.."
cd %parent_dir2%
set "parent_dir2=%cd%"


set "parent_dir3=%parent_dir2%\.."
cd %parent_dir3%
set "parent_dir3=%cd%"


set WORKSPACE=%parent_dir2%
set PARENT_WORKSPACE=%parent_dir3%

set GEN_CLIENT=%WORKSPACE%\Luban\Luban.ClientServer\Luban.ClientServer.exe
set CONF_ROOT=%WORKSPACE%\Luban

%GEN_CLIENT% -j cfg --^
 -d %CONF_ROOT%\ConfigRoot\Defines\__root__.xml ^
 --input_data_dir %CONF_ROOT%\ConfigRoot\Datas ^
 --output_data_dir %WORKSPACE%\server_json ^
 --gen_types data_json ^

 -s server