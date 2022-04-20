#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/DF.RealEstate.Web.Host/DF.RealEstate.Web.Host.csproj", "src/DF.RealEstate.Web.Host/"]
COPY ["src/DF.RealEstate.Web.Core/DF.RealEstate.Web.Core.csproj", "src/DF.RealEstate.Web.Core/"]
COPY ["src/DF.RealEstate.Application/DF.RealEstate.Application.csproj", "src/DF.RealEstate.Application/"]
COPY ["src/DF.RealEstate.Application.Shared/DF.RealEstate.Application.Shared.csproj", "src/DF.RealEstate.Application.Shared/"]
COPY ["src/DF.RealEstate.Core.Shared/DF.RealEstate.Core.Shared.csproj", "src/DF.RealEstate.Core.Shared/"]
COPY ["src/DF.RealEstate.Core/DF.RealEstate.Core.csproj", "src/DF.RealEstate.Core/"]
COPY ["src/DF.RealEstate.EntityFrameworkCore/DF.RealEstate.EntityFrameworkCore.csproj", "src/DF.RealEstate.EntityFrameworkCore/"]
COPY ["src/DF.RealEstate.GraphQL/DF.RealEstate.GraphQL.csproj", "src/DF.RealEstate.GraphQL/"]
RUN dotnet restore "src/DF.RealEstate.Web.Host/DF.RealEstate.Web.Host.csproj"
COPY . .
WORKDIR "/src/src/DF.RealEstate.Web.Host"
RUN dotnet build "DF.RealEstate.Web.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DF.RealEstate.Web.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DF.RealEstate.Web.Host.dll"]
