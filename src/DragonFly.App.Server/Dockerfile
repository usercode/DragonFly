#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
RUN sed -i'.bak' 's/$/ contrib/' /etc/apt/sources.list
RUN apt-get update
RUN apt-get install -y ttf-mscorefonts-installer fontconfig
RUN apt-get install -y --no-install-recommends libgdiplus libc6-dev \
   && apt-get clean \
   && rm -rf /var/lib/apt/lists/*
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DragonFly.App.Server/DragonFly.App.Server.csproj", "DragonFly.App.Server/"]
COPY ["DragonFly.ApiKeys.AspNetCore/DragonFly.ApiKeys.AspNetCore.csproj", "DragonFly.ApiKeys.AspNetCore/"]
COPY ["DragonFly.ApiKeys/DragonFly.ApiKeys.csproj", "DragonFly.ApiKeys/"]
COPY ["DragonFly.Core/DragonFly.Core.csproj", "DragonFly.Core/"]
COPY ["DragonFly.Permissions.AspNetCore/DragonFly.Permissions.AspNetCore.csproj", "DragonFly.Permissions.AspNetCore/"]
COPY ["DragonFly.AspNetCore/DragonFly.AspNetCore.csproj", "DragonFly.AspNetCore/"]
COPY ["DragonFly.API.Client/DragonFly.API.Client.csproj", "DragonFly.API.Client/"]
COPY ["DragonFly.API.Exports/DragonFly.API.Exports.csproj", "DragonFly.API.Exports/"]
COPY ["DragonFly.Permissions/DragonFly.Permissions.csproj", "DragonFly.Permissions/"]
COPY ["DragonFly.API/DragonFly.API.csproj", "DragonFly.API/"]
COPY ["DragonFly.App.Client/DragonFly.App.Client.csproj", "DragonFly.App.Client/"]
COPY ["DragonFly.ApiKeys.Razor/DragonFly.ApiKeys.Razor.csproj", "DragonFly.ApiKeys.Razor/"]
COPY ["DragonFly.Permissions.Client/DragonFly.Permissions.Client.csproj", "DragonFly.Permissions.Client/"]
COPY ["DragonFly.Permissions.Razor/DragonFly.Permissions.Razor.csproj", "DragonFly.Permissions.Razor/"]
COPY ["DragonFly.Razor/DragonFly.Razor.csproj", "DragonFly.Razor/"]
COPY ["DragonFly.BlockField.Razor/DragonFly.BlockField.Razor.csproj", "DragonFly.BlockField.Razor/"]
COPY ["DragonFly.BlockField/DragonFly.BlockField.csproj", "DragonFly.BlockField/"]
COPY ["DragonFly.Proxy.Attributes/DragonFly.Proxy.Attributes.csproj", "DragonFly.Proxy.Attributes/"]
COPY ["DragonFly.Identity.Razor/DragonFly.Identity.Razor.csproj", "DragonFly.Identity.Razor/"]
COPY ["DragonFly.Identity/DragonFly.Identity.csproj", "DragonFly.Identity/"]
COPY ["DragonFly.BlockField.AspNetCore/DragonFly.BlockField.AspNetCore.csproj", "DragonFly.BlockField.AspNetCore/"]
COPY ["DragonFly.GraphQL/DragonFly.GraphQL.csproj", "DragonFly.GraphQL/"]
COPY ["DragonFly.Identity.AspNetCore/DragonFly.Identity.AspNetCore.csproj", "DragonFly.Identity.AspNetCore/"]
COPY ["DragonFly.ImageWizard/DragonFly.ImageWizard.csproj", "DragonFly.ImageWizard/"]
COPY ["DragonFly.MongoDB/DragonFly.MongoDB.csproj", "DragonFly.MongoDB/"]
COPY ["DragonFly.Proxy/DragonFly.Proxy.csproj", "DragonFly.Proxy/"]
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