#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DragonFly.App.Server/DragonFly.App.Server.csproj", "DragonFly.App.Server/"]
COPY ["DragonFly.Permissions.AspNetCore/DragonFly.Permissions.AspNetCore.csproj", "DragonFly.Permissions.AspNetCore/"]
COPY ["DragonFly.AspNetCore/DragonFly.AspNetCore.csproj", "DragonFly.AspNetCore/"]
COPY ["DragonFly.Core/DragonFly.Core.csproj", "DragonFly.Core/"]
COPY ["DragonFly.AspNetCore.API.Client/DragonFly.AspNetCore.API.Client.csproj", "DragonFly.AspNetCore.API.Client/"]
COPY ["DragonFly.AspNetCore.API.Exports/DragonFly.AspNetCore.API.Exports.csproj", "DragonFly.AspNetCore.API.Exports/"]
COPY ["DragonFly.Permissions/DragonFly.Permissions.csproj", "DragonFly.Permissions/"]
COPY ["DragonFly.AspNetCore.SchemaBuilder/DragonFly.AspNetCore.SchemaBuilder.csproj", "DragonFly.AspNetCore.SchemaBuilder/"]
COPY ["DragonFly.Fields.BlockField.AspNetCore/DragonFly.Fields.BlockField.AspNetCore.csproj", "DragonFly.Fields.BlockField.AspNetCore/"]
COPY ["DragonFly.Fields.BlockField/DragonFly.Fields.BlockField.csproj", "DragonFly.Fields.BlockField/"]
COPY ["DragonFly.ImageWizard/DragonFly.ImageWizard.csproj", "DragonFly.ImageWizard/"]
COPY ["DragonFly.ApiKeys.AspNetCore/DragonFly.ApiKeys.AspNetCore.csproj", "DragonFly.ApiKeys.AspNetCore/"]
COPY ["DragonFly.ApiKeys/DragonFly.ApiKeys.csproj", "DragonFly.ApiKeys/"]
COPY ["DragonFly.App.Client/DragonFly.App.Client.csproj", "DragonFly.App.Client/"]
COPY ["DragonFly.Identity.Razor/DragonFly.Identity.Razor.csproj", "DragonFly.Identity.Razor/"]
COPY ["DragonFly.Identity/DragonFly.Identity.csproj", "DragonFly.Identity/"]
COPY ["DragonFly.Permissions.Client/DragonFly.Permissions.Client.csproj", "DragonFly.Permissions.Client/"]
COPY ["DragonFly.Razor/DragonFly.Razor.csproj", "DragonFly.Razor/"]
COPY ["DragonFly.Permissions.Razor/DragonFly.Permissions.Razor.csproj", "DragonFly.Permissions.Razor/"]
COPY ["DragonFly.Fields.BlockField.Razor/DragonFly.Fields.BlockField.Razor.csproj", "DragonFly.Fields.BlockField.Razor/"]
COPY ["DragonFly.ApiKeys.Razor/DragonFly.ApiKeys.Razor.csproj", "DragonFly.ApiKeys.Razor/"]
COPY ["DragonFly.AspNetCore.GraphQL/DragonFly.AspNetCore.GraphQL.csproj", "DragonFly.AspNetCore.GraphQL/"]
COPY ["DragonFly.Storage.MongoDB/DragonFly.Storage.MongoDB.csproj", "DragonFly.Storage.MongoDB/"]
COPY ["DragonFly.AspNetCore.API/DragonFly.AspNetCore.API.csproj", "DragonFly.AspNetCore.API/"]
COPY ["DragonFly.SampleData/DragonFly.SampleData.csproj", "DragonFly.SampleData/"]
COPY ["DragonFly.Identity.AspNetCore/DragonFly.Identity.AspNetCore.csproj", "DragonFly.Identity.AspNetCore/"]
RUN dotnet restore "DragonFly.App.Server/DragonFly.App.Server.csproj"
COPY . .
WORKDIR "/src/DragonFly.App.Server"
RUN dotnet build "DragonFly.App.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DragonFly.App.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DragonFly.App.Server.dll"]