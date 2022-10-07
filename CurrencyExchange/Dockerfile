FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine as base
WORKDIR /app
EXPOSE 80

RUN apk add --no-cache tzdata

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -c release -o /build --no-restore

FROM base as runtime
WORKDIR /app
COPY --from=build /build .
ENTRYPOINT ["dotnet", "CurrencyExchange.dll"]