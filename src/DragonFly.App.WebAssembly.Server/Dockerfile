#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DragonFly.App.WebAssembly.Server/DragonFly.App.WebAssembly.Server.csproj", "DragonFly.App.WebAssembly.Server/"]
COPY ["DragonFly.API/DragonFly.API.csproj", "DragonFly.API/"]
COPY ["DragonFly.API.Core/DragonFly.API.Core.csproj", "DragonFly.API.Core/"]
COPY ["DragonFly.Core/DragonFly.Core.csproj", "DragonFly.Core/"]
COPY ["DragonFly.Generator/DragonFly.Generator.csproj", "DragonFly.Generator/"]
COPY ["DragonFly.AspNetCore/DragonFly.AspNetCore.csproj", "DragonFly.AspNetCore/"]
COPY ["DragonFly.App.WebAssembly.Client/DragonFly.App.WebAssembly.Client.csproj", "DragonFly.App.WebAssembly.Client/"]
COPY ["DragonFly.Client.WebAssembly/DragonFly.Client.WebAssembly.csproj", "DragonFly.Client.WebAssembly/"]
COPY ["DragonFly.Client/DragonFly.Client.csproj", "DragonFly.Client/"]
COPY ["DragonFly.API.Client/DragonFly.API.Client.csproj", "DragonFly.API.Client/"]
COPY ["DragonFly.ApiKeys.Client/DragonFly.ApiKeys.Client.csproj", "DragonFly.ApiKeys.Client/"]
COPY ["DragonFly.ApiKeys.Core/DragonFly.ApiKeys.Core.csproj", "DragonFly.ApiKeys.Core/"]
COPY ["DragonFly.Identity.Client/DragonFly.Identity.Client.csproj", "DragonFly.Identity.Client/"]
COPY ["DragonFly.Identity.Core/DragonFly.Identity.Core.csproj", "DragonFly.Identity.Core/"]
COPY ["DragonFly.ImageWizard/DragonFly.ImageWizard.csproj", "DragonFly.ImageWizard/"]
COPY ["DragonFly.MongoDB/DragonFly.MongoDB.csproj", "DragonFly.MongoDB/"]
COPY ["DragonFly.Identity/DragonFly.Identity.csproj", "DragonFly.Identity/"]
COPY ["DragonFly.ApiKeys/DragonFly.ApiKeys.csproj", "DragonFly.ApiKeys/"]
COPY ["DragonFly.SampleData/DragonFly.SampleData.csproj", "DragonFly.SampleData/"]
RUN dotnet restore "./DragonFly.App.WebAssembly.Server/DragonFly.App.WebAssembly.Server.csproj"
COPY . .
WORKDIR "/src/DragonFly.App.WebAssembly.Server"
RUN dotnet build "./DragonFly.App.WebAssembly.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DragonFly.App.WebAssembly.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DragonFly.App.WebAssembly.Server.dll"]