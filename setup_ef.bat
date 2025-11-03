@echo off
echo Installing EF Core packages...
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools
echo.
echo Packages installed successfully!
echo.
echo Now you can run Scaffold-DbContext command manually.
echo Example:
echo Scaffold-DbContext "Data Source=Korzh;Initial Catalog=Pr8_Gordov_Main;Integrated Security=True;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer
pause