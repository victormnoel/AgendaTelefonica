# Agenda Telefônica

Este projeto é uma aplicação de Agenda Telefônica, com o objetivo é gerenciar contatos, permitindo cadastro, edição, exclusão e busca de contatos telefônicos.

## Funcionalidades

- Cadastro de novos contatos
- Edição de contatos existentes
- Exclusão de contatos
- Busca de contatos por nome, telefone ou outros critérios
- Listagem de todos os contatos
- Validação de dados de entrada

## Tecnologias Utilizadas

**Backend**
- .NET Web ASP.NET Core (C#)
- FluentValidation (validação do modelo de entrada)

**Frontend**
- Vue.js

**Banco de Dados**
- MySQL

**Documentação e Testes de API**
- Swagger (documentação e testes interativos dos endpoints)
  
**Testes**
- xUnit (testes unitários)
- FakeItEasy (mock)
- Bogus / AutoFixture (carga de teste)

**Gerenciamento**
- NuGet (dependências)
- Git / GitHub (controle de versão)

## Padrões e Boas Práticas

- **Arquitetura Limpa:** Separação clara entre camadas de domínio, aplicação, infraestrutura e apresentação.
- **DDD (Domain-Driven Design):** Organização do código baseada em domínios de negócio.
- **CQRS (Command Query Responsibility Segregation):** Separação entre operações de escrita (commands) e leitura (queries), garantindo maior clareza, escalabilidade e manutenibilidade.
- **Repository Pattern:** Abstração do acesso a dados.
- **Injeção de Dependência:** Facilita o desacoplamento entre componentes.
- **Validação de Dados:** Garantia de integridade dos dados inseridos pelo usuário.
- **Testes Unitários:** Cobertura das principais funcionalidades para garantir a qualidade do código.
- **Documentação com Swagger:** Disponibilização de interface interativa para explorar e testar os endpoints da API.
- **Componentização no Frontend:** Reutilização de componentes Vue.js para garantir coesão, consistência visual e facilidade de manutenção.


