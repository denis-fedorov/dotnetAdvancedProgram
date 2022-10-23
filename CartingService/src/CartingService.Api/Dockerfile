FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CartingService.Api/CartingService.Api.csproj", "CartingService.Api/"]
RUN dotnet restore "CartingService.Api/CartingService.Api.csproj"
COPY . .
WORKDIR "/src/CartingService.Api"
RUN dotnet build "CartingService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CartingService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CartingService.Api.dll"]
