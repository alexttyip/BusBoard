FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY TramBoard/*.csproj ./TramBoard/
COPY TramBoard.API/*.csproj ./TramBoard.API/
COPY TramBoard.WEB/*.csproj ./TramBoard.WEB/

RUN dotnet restore

# Copy everything else and build
COPY . .

WORKDIR /app/TramBoard.API
RUN dotnet publish -c Release -o /app/out

WORKDIR /app/TramBoard.WEB
RUN dotnet publish -c Release -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Run the app on container startup
ENTRYPOINT ["dotnet", "TramBoard.WEB.dll"]
