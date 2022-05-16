# API UPLOAD BOLETOS

## Frameworks

- Entity Framework
- FluenteValidation
- xUnit
- FluentAssertions
- Swashbuckle

## Arquitetura e Patterns

- A aplicação aplica conceitos da [Arquitetura Limpa](https://docs.microsoft.com/pt-br/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture)
- SOLID e Clean Code
- DDD (camadas e Domain Model Pattern)
- Domain Validations
- Builder
- Repository

## Referencias

- https://docs.microsoft.com/pt-br/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture
- https://docs.microsoft.com/pt-br/dotnet/csharp/language-reference/operators/null-coalescing-operator
- https://rules.sonarsource.com/csharp/RSPEC-107
- https://refactoring.guru/pt-br/design-patterns/builder
- https://balta.io/blog/aspnet-minimal-apis
- https://docs.microsoft.com/pt-br/dotnet/csharp/fundamentals/exceptions/creating-and-throwing-exceptions
- https://ardalis.com/enum-alternatives-in-c/
- https://lostechies.com/jimmybogard/2008/08/12/enumeration-classes/
- https://medium.com/swlh/2-defensive-coding-techniques-you-should-use-today-4225cacc1c29
- https://hub.docker.com/_/microsoft-mssql-server
- https://opentelemetry.io/docs/instrumentation/net/exporters/

## Oportunidades de melhorias

- Aumentar a cobertura de testes
- Monitoramento e Log
- Autorização

## Como executar

1. Banco de dados

   `docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-CU29-ubuntu-16.04`

2. Monitoramento do banco de dados

   `docker run --rm -ti --name=ctop --volume /var/run/docker.sock:/var/run/docker.sock:ro quay.io/vektorlab/ctop:latest`

3. Criar o database (Migration)

   `add-migration Initial`

   `update-database`

4. Monitoramento da aplicação

   `docker run -p 9090:9090 -v ${PWD}/prometheus.yml:/etc/prometheus/prometheus.yml prom/prometheus`
