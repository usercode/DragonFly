#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DragonFlyTemplate/DragonFlyTemplate.csproj", "DragonFlyTemplate/"]
COPY ["DragonFly.AspNetCore.API/DragonFly.AspNetCore.API.csproj", "DragonFly.AspNetCore.API/"]
COPY ["DragonFly.AspNetCore.API.Exports/DragonFly.AspNetCore.API.Exports.csproj", "DragonFly.AspNetCore.API.Exports/"]
COPY ["DragonFly.Core/DragonFly.Core.csproj", "DragonFly.Core/"]
COPY ["DragonFly.AspNetCore/DragonFly.AspNetCore.csproj", "DragonFly.AspNetCore/"]
COPY ["DragonFly.AspNetCore.API.Client/DragonFly.AspNetCore.API.Client.csproj", "DragonFly.AspNetCore.API.Client/"]
COPY ["DragonFly.AspNetCore.SchemaBuilder/DragonFly.AspNetCore.SchemaBuilder.csproj", "DragonFly.AspNetCore.SchemaBuilder/"]
COPY ["DragonFly.AspNetCore.SchemaBuilder.Attributes/DragonFly.AspNetCore.SchemaBuilder.Attributes.csproj", "DragonFly.AspNetCore.SchemaBuilder.Attributes/"]
COPY ["DragonFly.Fields.BlockField.AspNetCore/DragonFly.Fields.BlockField.AspNetCore.csproj", "DragonFly.Fields.BlockField.AspNetCore/"]
COPY ["DragonFly.Fields.BlockField/DragonFly.Fields.BlockField.csproj", "DragonFly.Fields.BlockField/"]
COPY ["DragonFly.Identity.AspNetCore/DragonFly.Identity.AspNetCore.csproj", "DragonFly.Identity.AspNetCore/"]
COPY ["DragonFly.Identity/DragonFly.Identity.csproj", "DragonFly.Identity/"]
COPY ["DragonFly.Permissions.AspNetCore/DragonFly.Permissions.AspNetCore.csproj", "DragonFly.Permissions.AspNetCore/"]
COPY ["DragonFly.Permissions/DragonFly.Permissions.csproj", "DragonFly.Permissions/"]
COPY ["DragonFly.ImageWizard/DragonFly.ImageWizard.csproj", "DragonFly.ImageWizard/"]
COPY ["DragonFly.Storage.MongoDB/DragonFly.Storage.MongoDB.csproj", "DragonFly.Storage.MongoDB/"]
COPY ["DragonFlyTemplate.Client/DragonFlyTemplate.Client.csproj", "DragonFlyTemplate.Client/"]
COPY ["DragonFly.ApiKeys.Razor/DragonFly.ApiKeys.Razor.csproj", "DragonFly.ApiKeys.Razor/"]
COPY ["DragonFly.ApiKeys/DragonFly.ApiKeys.csproj", "DragonFly.ApiKeys/"]
COPY ["DragonFly.Permissions.Client/DragonFly.Permissions.Client.csproj", "DragonFly.Permissions.Client/"]
COPY ["DragonFly.Permissions.Razor/DragonFly.Permissions.Razor.csproj", "DragonFly.Permissions.Razor/"]
COPY ["DragonFly.Razor/DragonFly.Razor.csproj", "DragonFly.Razor/"]
COPY ["DragonFly.Fields.BlockField.Razor/DragonFly.Fields.BlockField.Razor.csproj", "DragonFly.Fields.BlockField.Razor/"]
COPY ["DragonFly.Identity.Razor/DragonFly.Identity.Razor.csproj", "DragonFly.Identity.Razor/"]
RUN dotnet restore "DragonFlyTemplate/DragonFlyTemplate.csproj"
COPY . .
WORKDIR "/src/DragonFlyTemplate"
RUN dotnet build "DragonFlyTemplate.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DragonFlyTemplate.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DragonFlyTemplate.dll"]