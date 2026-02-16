# Etapa 1. Construção da aplicação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

#Etapa 2. RUNTIME - Configuração do ambiente de execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 
WORKDIR /app
COPY --from=build /app/out .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "PecasAntunes.API.dll"]
