# Imagem base para o runtime ASP.NET Core
FROM mcr.microsoft.com/dotnet/aspnet:8.0@sha256:3a305bc84767bbb651bd035119fe319a91fe927155706f7296499ca0205c812a AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Imagem base para o SDK .NET (compilar e buildar)
FROM mcr.microsoft.com/dotnet/sdk:8.0@sha256:2d7f935b8c7fe032cd3d36b5ce9c82c24413881e6dad1b4fbdf36cf369e4244f AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./CRUDPessoaEmpresa.Teste.API/CRUDPessoaEmpresa.Teste.API.csproj", "CRUDPessoaEmpresa.Teste.API/"]
RUN dotnet restore "./CRUDPessoaEmpresa.Teste.API/CRUDPessoaEmpresa.Teste.API.csproj"
COPY . .
WORKDIR "/src/CRUDPessoaEmpresa.Teste.API"
RUN dotnet build "./CRUDPessoaEmpresa.Teste.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publicação do projeto
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CRUDPessoaEmpresa.Teste.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Imagem final para execução
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRUDPessoaEmpresa.Teste.API.dll"]
