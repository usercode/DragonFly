#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["DragonFly.App.Server/DragonFly.App.Server.csproj", "DragonFly.App.Server/"]
COPY ["DragonFly.AspNetCore.SchemaBuilder/DragonFly.AspNetCore.SchemaBuilder.csproj", "DragonFly.AspNetCore.SchemaBuilder/"]
COPY ["DragonFly.AspNetCore/DragonFly.AspNetCore.csproj", "DragonFly.AspNetCore/"]
COPY ["DragonFly.Core/DragonFly.Core.csproj", "DragonFly.Core/"]
COPY ["DragonFly.AspNetCore.API.Client/DragonFly.AspNetCore.API.Client.csproj", "DragonFly.AspNetCore.API.Client/"]
COPY ["DragonFly.AspNetCore.API.Exports/DragonFly.AspNetCore.API.Exports.csproj", "DragonFly.AspNetCore.API.Exports/"]
COPY ["DragonFly.App.Client/DragonFly.App.Client.csproj", "DragonFly.App.Client/"]
COPY ["DragonFly.Razor/DragonFly.Razor.csproj", "DragonFly.Razor/"]
COPY ["DragonFly.AspNetCore.GraphQL/DragonFly.AspNetCore.GraphQL.csproj", "DragonFly.AspNetCore.GraphQL/"]
COPY ["DragonFly.Storage.MongoDB/DragonFly.Storage.MongoDB.csproj", "DragonFly.Storage.MongoDB/"]
COPY ["DragonFly.AspNetCore.API/DragonFly.AspNetCore.API.csproj", "DragonFly.AspNetCore.API/"]
COPY ["DragonFly.SampleData/DragonFly.SampleData.csproj", "DragonFly.SampleData/"]
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