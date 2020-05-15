#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["api/basket/eShop.Basket/eShop.Basket.csproj", "api/basket/eShop.Basket/"]
COPY ["commun/eShop.Common/eShop.Common.csproj", "commun/eShop.Common/"]
RUN dotnet restore "api/basket/eShop.Basket/eShop.Basket.csproj"
COPY . .
WORKDIR "/src/api/basket/eShop.Basket"
RUN dotnet build "eShop.Basket.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eShop.Basket.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShop.Basket.dll"]