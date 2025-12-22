# Use Cases

Esta pasta contém os casos de uso (aplicação) da arquitetura hexagonal.

## Estrutura

Os casos de uso devem ser organizados por domínio/agregado:

```
UseCases/
├── [Domínio]/
│   ├── Create[Domínio]UseCase.cs
│   ├── Update[Domínio]UseCase.cs
│   ├── Delete[Domínio]UseCase.cs
│   ├── Get[Domínio]UseCase.cs
│   └── List[Domínios]UseCase.cs
```

## Exemplo

```csharp
public class CreateProductUseCase
{
    private readonly IRepository<Product> _repository;

    public CreateProductUseCase(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<ProductDTO> ExecuteAsync(CreateProductDTO input)
    {
        // Lógica do caso de uso
    }
}
```
