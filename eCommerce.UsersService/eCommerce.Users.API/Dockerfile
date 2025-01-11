FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["eCommerce.Users.API/eCommerce.Users.API.csproj", "eCommerce.Users.API/"]
COPY ["eCommerce.Users.Infrastructure/eCommerce.Users.Infrastructure.csproj", "eCommerce.Users.Infrastructure/"]
COPY ["eCommerce.Users.Core/eCommerce.Users.Core.csproj", "eCommerce.Users.Core/"]
RUN dotnet restore "eCommerce.Users.API/eCommerce.Users.API.csproj"
COPY . .
WORKDIR "/src/eCommerce.Users.API"
RUN dotnet build "eCommerce.Users.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "eCommerce.Users.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.Users.API.dll"]
