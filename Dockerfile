FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
# Install curl for healthcheck
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["EFCoreIsitech.csproj", "./"]
RUN dotnet restore "EFCoreIsitech.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "EFCoreIsitech.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EFCoreIsitech.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EFCoreIsitech.dll"]