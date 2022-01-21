FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/services/LV.Identidade.API/LV.Identidade.API.csproj", "LV.Identidade.API/"]
RUN dotnet restore "src/services/LV.Identidade.API/LV.Identidade.API.csproj"
COPY . .
WORKDIR "/src/LV.Identidade.API"
RUN dotnet build "LV.Identidade.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LV.Identidade.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LV.Identidade.API.dll"]
