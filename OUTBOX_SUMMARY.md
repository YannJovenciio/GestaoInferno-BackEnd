# Outbox Flow Summary

## Diagrama (Mermaid)
```mermaid
flowchart TD
    A[HTTP POST /api/Sin] --> B[SinUseCase.CreateSin]
    B -->|Cria Sin| C[DB]
    B -->|Publica SinCreatedEvent| D[OutBoxEvent (ProcessedAt=null)]
    D -->|Retorna 201 rápido| A

    subgraph Dispatcher Loop
      E[OutboxDispatcherService] --> F[Busca OutBoxEvent pendentes]
      F --> G[Deserializa Content -> SinCreatedEvent]
      G --> H[Resolve IEventHandler<SinCreatedEvent> via scope]
      H --> I[SinCreatedHandler]
      I -->|severidade >= high| J[Seleciona Demon + Soul]
      J -->|Idempotência check| K[Cria Persecution]
      K --> L[Atualiza OutBoxEvent: ProcessedAt=UtcNow, Attempts++, Error=null]
      I -->|severity baixa| M[Ignora/Loga]
      G -->|tipo desconhecido| N[Attempts++, Error="unknown type"]
      G -->|erro| O[Attempts++, Error=ex.Message]
    end

    L --> P[Logs de sucesso]
    N --> Q[Logs warning tipo desconhecido]
    O --> R[Logs erro por EventId]
```

## Passo a passo lógico
1) Entidade/tabela OutBoxEvent
   - Campos: Id, Type, Content, CreatedAt, ProcessedAt (nullable), Attempts, Error.
   - Config EF: ProcessedAt opcional em `src/Configuration/OutBoxEvent/OutBoxEventConfiguration.cs`.

2) DbContext + Migration
   - `DbSet<OutBoxEvent>` no HellDbContext.
   - Migration aplicada (ProcessedAt nullable).

3) Domain Event
   - `IDomainEvent` e `SinCreatedEvent` em `src/Core/Domain/Event`.

4) Publisher
   - `OutBoxEventPublisher` serializa evento e insere na tabela OutBoxEvent.

5) Use case Sin
   - Após criar Sin, chama `PublishAsync(new SinCreatedEvent(...))` em `SinUseCase`.

6) Handler
   - `SinCreatedHandler` (`IEventHandler<SinCreatedEvent>`): severidade >= high, escolhe Demon/Soul, idempotência simples, cria Persecution.

7) Dispatcher
   - `OutboxDispatcherService` (HostedService): loop com delay, busca pendentes, desserializa, resolve handler via scope, atualiza ProcessedAt/Attempts/Error, logs estruturados.

8) DI/Wiring
   - `IEventPublisher -> OutBoxEventPublisher`.
   - `IEventHandler<SinCreatedEvent> -> SinCreatedHandler`.
   - `OutboxDispatcherService` registrado como hosted service em `Program.cs`.

9) Teste manual
   - POST Sin (Severity high/critical).
   - Verificar OutBoxEvent inserido; dispatcher processa; Persecution criada; ProcessedAt preenchido; logs de sucesso.
