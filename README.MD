dotnet --version
dotnet --list-sdks
dotnet new globaljson --sdk-version 8.0.406
dotnet new sln -n PecasAntunesApi

mkdir src

mkdir src\PecasAntunes.Domain

mkdir src\PecasAntunes.Application
mkdir src\PecasAntunes.Infrastructure
mkdir src\PecasAntunes.API

dotnet new classlib -n PecasAntunes.Domain -o src\PecasAntunes.Domain
dotnet new classlib -n PecasAntunes.Application -o src\PecasAntunes.Application
dotnet new classlib -n PecasAntunes.Infrastructure -o src\PecasAntunes.Infrastructure
dotnet new webapi -n PecasAntunes.API -o src\PecasAntunes.API

dotnet sln PecasAntunesApi.sln add src\PecasAntunes.Domain\PecasAntunes.Domain.csproj

Rename-Item src\PecasAntunes.Application\PecasAntunes.Doamin.csproj PecasAntunes.Application.csprojcls

dir src\PecasAntunes.Infrastructure





