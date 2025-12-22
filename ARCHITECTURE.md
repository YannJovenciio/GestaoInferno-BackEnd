# Arquitetura Hexagonal - Inferno CRUD

## Estrutura do Projeto

```
src/
├── Core/                          # Núcleo da aplicação (lógica de negócio)
│   ├── Domain/                    # Camada de domínio
│   │   ├── Entities/              # Entidades do domínio
│   │   └── Interfaces/            # Interfaces (Portas)
│   └── Application/               # Camada de aplicação
│       ├── UseCases/              # Casos de uso
│       └── DTOs/                  # Data Transfer Objects
│
├── Adapters/                      # Adaptadores (implementações)
│   ├── Inbound/                   # Adaptadores de entrada
│   │   └── Controllers/           # Controllers HTTP
│   └── Outbound/                  # Adaptadores de saída
│       └── Persistence/           # Persistência de dados
│           └── Repositories/      # Repositórios
│
└── Configuration/                 # Configuração e injeção de dependência
```

## Camadas

### 1. **Domain (Núcleo)**
- **Entities**: Contém as entidades de negócio
- **Interfaces (Portas)**: Define contratos para comunicação com adaptadores

### 2. **Application**
- **UseCases**: Lógica de negócio, regras da aplicação
- **DTOs**: Transferência de dados entre camadas

### 3. **Adapters (Periféricos)**
- **Inbound**: Pontos de entrada (Controllers, APIs)
- **Outbound**: Implementações externas (Banco de dados, APIs externas)

### 4. **Configuration**
- Configuração de serviços e injeção de dependência

## Fluxo de Dados

```
Usuário
  ↓
Controllers (Inbound Adapter)
  ↓
UseCases (Application)
  ↓
Entities (Domain)
  ↓
Repositories (Outbound Adapter)
  ↓
Banco de Dados
```

## Benefícios

✅ **Independência de Framework**: Mudança fácil de tecnologias
✅ **Testabilidade**: Fácil criar testes unitários
✅ **Manutenibilidade**: Código organizado e escalável
✅ **Reutilização**: Componentes desacoplados
