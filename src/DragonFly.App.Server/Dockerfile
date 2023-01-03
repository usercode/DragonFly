#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DragonFly.App.Server/DragonFly.App.Server.csproj", "DragonFly.App.Server/"]
COPY ["DragonFly.App.Client/DragonFly.App.Client.csproj", "DragonFly.App.Client/"]
COPY ["DragonFly.Client/DragonFly.Client.csproj", "DragonFly.Client/"]
COPY ["DragonFly.Core/DragonFly.Core.csproj", "DragonFly.Core/"]
COPY ["DragonFly.API.Client/DragonFly.API.Client.csproj", "DragonFly.API.Client/"]
COPY ["DragonFly.API.Core/DragonFly.API.Core.csproj", "DragonFly.API.Core/"]
COPY ["DragonFly.ApiKeys.Client/DragonFly.ApiKeys.Client.csproj", "DragonFly.ApiKeys.Client/"]
COPY ["DragonFly.ApiKeys.Core/DragonFly.ApiKeys.Core.csproj", "DragonFly.ApiKeys.Core/"]
COPY ["DragonFly.Permissions.Client/DragonFly.Permissions.Client.csproj", "DragonFly.Permissions.Client/"]
COPY ["DragonFly.Permissions.Core/DragonFly.Permissions.Core.csproj", "DragonFly.Permissions.Core/"]
COPY ["DragonFly.BlockField.Client/DragonFly.BlockField.Client.csproj", "DragonFly.BlockField.Client/"]
COPY ["DragonFly.BlockField.Core/DragonFly.BlockField.Core.csproj", "DragonFly.BlockField.Core/"]
COPY ["DragonFly.Proxy.Attributes/DragonFly.Proxy.Attributes.csproj", "DragonFly.Proxy.Attributes/"]
COPY ["DragonFly.Identity.Client/DragonFly.Identity.Client.csproj", "DragonFly.Identity.Client/"]
COPY ["DragonFly.Identity.Core/DragonFly.Identity.Core.csproj", "DragonFly.Identity.Core/"]
COPY ["DragonFly.API/DragonFly.API.csproj", "DragonFly.API/"]
COPY ["DragonFly.AspNetCore/DragonFly.AspNetCore.csproj", "DragonFly.AspNetCore/"]
COPY ["DragonFly.BlockField/DragonFly.BlockField.csproj", "DragonFly.BlockField/"]
COPY ["DragonFly.ImageWizard/DragonFly.ImageWizard.csproj", "DragonFly.ImageWizard/"]
COPY ["DragonFly.MongoDB/DragonFly.MongoDB.csproj", "DragonFly.MongoDB/"]
COPY ["DragonFly.Identity/DragonFly.Identity.csproj", "DragonFly.Identity/"]
COPY ["DragonFly.Permissions/DragonFly.Permissions.csproj", "DragonFly.Permissions/"]
COPY ["DragonFly.ApiKeys/DragonFly.ApiKeys.csproj", "DragonFly.ApiKeys/"]
COPY ["DragonFly.SampleData/DragonFly.SampleData.csproj", "DragonFly.SampleData/"]
RUN dotnet restore "DragonFly.App.Server/DragonFly.App.Server.csproj"
COPY . .
WORKDIR "/src/DragonFly.App.Server"
RUN dotnet build "DragonFly.App.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DragonFly.App.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DragonFly.App.Server.dll"]