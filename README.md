# Currency exchange

[![dotnet](https://github.com/GustasG/CurrencyExchange/actions/workflows/dotnet.yml/badge.svg)](https://github.com/GustasG/CurrencyExchange/actions/workflows/dotnet.yml)
[![codecov](https://codecov.io/gh/GustasG/CurrencyExchange/branch/master/graph/badge.svg?token=Lkh9nX6wLJ)](https://codecov.io/gh/GustasG/CurrencyExchange)
[![Codacy Badge](https://app.codacy.com/project/badge/Coverage/84f71dfd65be484b9b2ebcebe03fbabf)](https://www.codacy.com/gh/GustasG/CurrencyExchange/dashboard?utm_source=github.com&utm_medium=referral&utm_content=GustasG/CurrencyExchange&utm_campaign=Badge_Coverage)

Project for calculating exchange rates and historical exchange rates

## Instructions to launch

Using docker:

```bash
docker-compose up
```

Web client should be running on localhost:3000 for preview.

Note: Sometimes docker-compose will fail to set correct port for backend. If this happents try repeating command.

Using server/web client commands:

1. Launch server from CurrencyExchange directory using ```dotnet run``` command
2. Launch web client from currency-exchange-client directory using ```yarn start``` (after installing node modules using ```yarn install```)
3. Ensure that server is running on localhost:5000 or pass environment variable REACT_APP_SERVER_URL with running server URL
4. Web client should be awailable on localhost:3000 for preview

## Project infrastructure

1. currency-exchange-client - React front-end project
2. CurrencyExchange - dotnet back-end project
