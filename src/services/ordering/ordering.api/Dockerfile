#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["services/ordering/ordering.api/ordering.api.csproj", "services/ordering/ordering.api/"]
COPY ["services/ordering/ordering.application/ordering.application.csproj", "services/ordering/ordering.application/"]
COPY ["services/ordering/ordering.domain/ordering.domain.csproj", "services/ordering/ordering.domain/"]
COPY ["services/ordering/ordering.infrastructure/ordering.infrastructure.csproj", "services/ordering/ordering.infrastructure/"]
COPY ["common/EventBus.Messages/EventBus.Messages.csproj", "common/EventBus.Messages/"]
RUN dotnet restore "services/ordering/ordering.api/ordering.api.csproj"
COPY . .
WORKDIR "/src/services/ordering/ordering.api"
RUN dotnet build "ordering.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ordering.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ordering.api.dll"]
