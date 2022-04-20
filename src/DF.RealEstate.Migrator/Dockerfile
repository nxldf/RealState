#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/DF.RealEstate.Migrator/DF.RealEstate.Migrator.csproj", "src/DF.RealEstate.Migrator/"]
COPY ["src/DF.RealEstate.EntityFrameworkCore/DF.RealEstate.EntityFrameworkCore.csproj", "src/DF.RealEstate.EntityFrameworkCore/"]
COPY ["src/DF.RealEstate.Core/DF.RealEstate.Core.csproj", "src/DF.RealEstate.Core/"]
COPY ["src/DF.RealEstate.Core.Shared/DF.RealEstate.Core.Shared.csproj", "src/DF.RealEstate.Core.Shared/"]
RUN dotnet restore "src/DF.RealEstate.Migrator/DF.RealEstate.Migrator.csproj"
COPY . .
WORKDIR "/src/src/DF.RealEstate.Migrator"
RUN dotnet build "DF.RealEstate.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DF.RealEstate.Migrator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DF.RealEstate.Migrator.dll", "-s"]
