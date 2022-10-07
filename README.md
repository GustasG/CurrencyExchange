# Currency exchange

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
