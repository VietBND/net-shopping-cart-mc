#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["shopping-cart-apigateway/shopping-cart-apigateway.csproj", "shopping-cart-apigateway/"]
RUN dotnet restore "shopping-cart-apigateway/shopping-cart-apigateway.csproj"
COPY . .
WORKDIR "/src/shopping-cart-apigateway"
RUN dotnet build "shopping-cart-apigateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "shopping-cart-apigateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "shopping-cart-apigateway.dll"]