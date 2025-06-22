# ---------------------------------------------
# Build and test stage
# ---------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy everything and restore as distinct layers
COPY . ./
RUN dotnet build -c Release

# Run tests
RUN dotnet test --no-build -c Release

# Publish the app
RUN dotnet publish ./Billups.Api/Billups.Api.csproj -c Release -o /app/publish --no-restore

# ---------------------------------------------
# Runtime image
# ---------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish ./

EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

# swagger
ENV ASPNETCORE_ENVIRONMENT=Development 

ENTRYPOINT ["dotnet", "Billups.Api.dll"]
