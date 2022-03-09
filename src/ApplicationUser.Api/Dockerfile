#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApplicationUser.Api/ApplicationUser.Api.csproj", "ApplicationUser.Api/"]
COPY ["VietBND/VietBND.csproj", "VietBND/"]
COPY ["ApplicationUser.Services/ApplicationUser.Services.csproj", "ApplicationUser.Services/"]
COPY ["ApplicationUser.Infrastructures/ApplicationUser.Infrastructures.csproj", "ApplicationUser.Infrastructures/"]
COPY ["ApplicationUser.Domain/ApplicationUser.Domain.csproj", "ApplicationUser.Domain/"]
RUN dotnet restore "ApplicationUser.Api/ApplicationUser.Api.csproj"
COPY . .
WORKDIR "/src/ApplicationUser.Api"
RUN dotnet build "ApplicationUser.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApplicationUser.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApplicationUser.Api.dll"]
