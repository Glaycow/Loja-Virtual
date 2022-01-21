FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/web/LV.WebApp.MVC/LV.WebApp.MVC.csproj", "LV.WebApp.MVC/"]
RUN dotnet restore "src/web/LV.WebApp.MVC/LV.WebApp.MVC.csproj"
COPY . .
WORKDIR "/src/LV.WebApp.MVC"
RUN dotnet build "LV.WebApp.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LV.WebApp.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LV.WebApp.MVC.dll"]
