#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["services/discount.api/discount.api/discount.api.csproj", "services/discount.api/discount.api/"]
RUN dotnet restore "services/discount.api/discount.api/discount.api.csproj"
COPY . .
WORKDIR "/src/services/discount.api/discount.api"
RUN dotnet build "discount.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "discount.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "discount.api.dll"]
