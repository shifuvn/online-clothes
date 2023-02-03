#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim AS build
WORKDIR /src
COPY ["OnlineClothes.Api/OnlineClothes.Api.csproj", "OnlineClothes.Api/"]
COPY ["OnlineClothes.Application/OnlineClothes.Application.csproj", "OnlineClothes.Application/"]
COPY ["OnlineClothes.Domain/OnlineClothes.Domain.csproj", "OnlineClothes.Domain/"]
COPY ["OnlineClothes.BuildIn/OnlineClothes.BuildIn.csproj", "OnlineClothes.BuildIn/"]
COPY ["OnlineClothes.Infrastructure/OnlineClothes.Infrastructure.csproj", "OnlineClothes.Infrastructure/"]
COPY ["OnlineClothes.Persistence/OnlineClothes.Persistence.csproj", "OnlineClothes.Persistence/"]

RUN update-ca-certificates
RUN dotnet restore "OnlineClothes.Api/OnlineClothes.Api.csproj"
COPY . .

WORKDIR "/src/OnlineClothes.Api"
RUN dotnet build "OnlineClothes.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OnlineClothes.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OnlineClothes.Api.dll"]