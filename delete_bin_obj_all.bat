@echo off

call app_config.bat

CD %rootSourceCodePath%

@echo Deleting all BIN and OBJ project...
for /d /r . %%d in (bin,obj) do (
	if exist "%%d" (
		echo "%%d"
		rmdir /s/q "%%d"
	)
)
rmdir /s /q .vs
@echo BIN and OBJ project successfully deleted :) Close the window.

pause > nul