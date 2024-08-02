FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

COPY *.csproj .
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

EXPOSE 3005

ENTRYPOINT ["dotnet", "products.dll"]