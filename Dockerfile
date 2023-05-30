FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /Backend-QDAO

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore Backend-QDAO
# Build and publish a release
RUN dotnet publish Backend-QDAO -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /Backend-QDAO
COPY --from=build-env /Backend-QDAO/out .
ENTRYPOINT ["dotnet", "QDAO.Endpoint.dll"]
EXPOSE 80

