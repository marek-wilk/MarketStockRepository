#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["MarketStockAnalyzer.API/MarketStockAnalyzer.API.csproj", "MarketStockAnalyzer.API/"]
RUN dotnet restore "MarketStockAnalyzer.API/MarketStockAnalyzer.API.csproj"
COPY . .
WORKDIR "/src/MarketStockAnalyzer.API"
RUN dotnet build "MarketStockAnalyzer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MarketStockAnalyzer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MarketStockAnalyzer.API.dll"]
