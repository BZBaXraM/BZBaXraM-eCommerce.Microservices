FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["eCommerce.ProductsService/eCommerce.Products.API/eCommerce.Products.API.csproj", "eCommerce.ProductsService/eCommerce.Products.API/"]
COPY ["eCommerce.ProductsService/eCommerce.Products.BLL/eCommerce.Products.BLL.csproj", "eCommerce.ProductsService/eCommerce.Products.BLL/"]
COPY ["eCommerce.ProductsService/eCommerce.Products.DAL/eCommerce.Products.DAL.csproj", "eCommerce.ProductsService/eCommerce.Products.DAL/"]
RUN dotnet restore "eCommerce.ProductsService/eCommerce.Products.API/eCommerce.Products.API.csproj"
COPY . .
WORKDIR "/src/eCommerce.ProductsService/eCommerce.Products.API"
RUN dotnet build "eCommerce.Products.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "eCommerce.Products.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.Products.API.dll"]
