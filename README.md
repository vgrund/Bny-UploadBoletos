# API UPLOAD BOLETOS

## Frameworks

- Entity Framework
- FluenteValidation
- Swashbuckle

## Arquitetura e Patterns

- A aplicação aplica conceitos da [Arquitetura Limpa](https://docs.microsoft.com/pt-br/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture)
- SOLID e Clean Code
- DDD (camadas e Domain Model Pattern)
- Domain Events ?
- Domain Validations
- Builder
- Repository
- Unit of Work ?

## Referencias

- [Arquitetura Limpa](https://docs.microsoft.com/pt-br/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture)
- https://docs.microsoft.com/pt-br/dotnet/csharp/language-reference/operators/null-coalescing-operator
- https://rules.sonarsource.com/csharp/RSPEC-107
- https://refactoring.guru/pt-br/design-patterns/builder
- https://balta.io/blog/aspnet-minimal-apis
- https://docs.microsoft.com/pt-br/dotnet/csharp/fundamentals/exceptions/creating-and-throwing-exceptions
- https://ardalis.com/enum-alternatives-in-c/
- https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/
- https://medium.com/swlh/2-defensive-coding-techniques-you-should-use-today-4225cacc1c29
- https://hub.docker.com/_/microsoft-mssql-server

## Como executar

`docker pull mcr.microsoft.com/mssql/server:2017-latest`

`docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=pass123' -p 1401:1433 -n sql1 -d mcr.microsoft.com/mssql/server:2017-latest docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=pass123" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=pass123" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest `

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-CU29-ubuntu-16.04

docker run --rm -ti --name=ctop --volume /var/run/docker.sock:/var/run/docker.sock:ro quay.io/vektorlab/ctop:latest

add-migration Initial
update-database
