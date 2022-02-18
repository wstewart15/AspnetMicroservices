#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["api-gateways/shopping.aggregator/shopping.aggregator.csproj", "api-gateways/shopping.aggregator/"]
COPY ["common/Common.Logging/Common.Logging.csproj", "common/Common.Logging/"]
RUN dotnet restore "api-gateways/shopping.aggregator/shopping.aggregator.csproj"
COPY . .
WORKDIR "/src/api-gateways/shopping.aggregator"
RUN dotnet build "shopping.aggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "shopping.aggregator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "shopping.aggregator.dll"]