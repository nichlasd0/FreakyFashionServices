#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FreakyFashionServices.OrderProcessor/FreakyFashionServices.OrderProcessor.csproj", "FreakyFashionServices.OrderProcessor/"]
RUN dotnet restore "FreakyFashionServices.OrderProcessor/FreakyFashionServices.OrderProcessor.csproj"
COPY . .
WORKDIR "/src/FreakyFashionServices.OrderProcessor"
RUN dotnet build "FreakyFashionServices.OrderProcessor.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FreakyFashionServices.OrderProcessor.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreakyFashionServices.OrderProcessor.dll"]
