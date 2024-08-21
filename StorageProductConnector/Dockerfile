FROM mcr.microsoft.com/dotnet/sdk:latest AS build-env
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release  -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:latest
EXPOSE 8084
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "StorageProductConnector.dll"]