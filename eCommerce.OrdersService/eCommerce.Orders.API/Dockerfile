FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["eCommerce.OrdersService/eCommerce.Orders.API/eCommerce.Orders.API.csproj", "eCommerce.OrdersService/eCommerce.Orders.API/"]
COPY ["eCommerce.OrdersService/eCommerce.Orders.BLL/eCommerce.Orders.BLL.csproj", "eCommerce.OrdersService/eCommerce.Orders.BLL/"]
COPY ["eCommerce.OrdersService/eCommerce.Orders.DAL/eCommerce.Orders.DAL.csproj", "eCommerce.OrdersService/eCommerce.Orders.DAL/"]
RUN dotnet restore "eCommerce.OrdersService/eCommerce.Orders.API/eCommerce.Orders.API.csproj"
COPY . .
WORKDIR "/src/eCommerce.OrdersService/eCommerce.Orders.API"
RUN dotnet build "eCommerce.Orders.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "eCommerce.Orders.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.Orders.API.dll"]
