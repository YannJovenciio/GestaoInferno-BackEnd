# 🎯 Desafio: Sistema de Recomendação de Perseguição

## Contexto de Negócio

Você está evoluindo o sistema Inferno. Agora os **Administradores Demoníacos** precisam de um **dashboard de analytics** que mostre:

1. **Qual Demon está mais sobrecarregado?** (quantas Souls está perseguindo)
2. **Qual Soul está sendo mais perseguida?** (por quantos Demons)
3. **Ranking de eficiência**: Demons que perseguem Souls do mesmo nível (Inferior/Médio/Superior)
4. **Recomendações de novo assignment**: Sugerir o melhor par Demon/Soul baseado em:
   - Demon menos ocupado
   - Soul do mesmo nível de dificuldade (Level)
   - Categoria do Demon compatível com o tipo de Soul

---

## Desafio Técnico

### ❌ **Problema (Sem Eager Loading)**

```csharp
var demons = await _context.Demons.ToListAsync();  // Query 1

foreach (var demon in demons) {
    var count = demon.Persecutions.Count;  // Query 2, 3, 4, 5... (N+1 queries!)
    var souls = demon.Persecutions.Select(p => p.Soul).ToList();  // Mais queries!
}

// Resultado: 1 + (10 * 2) = 21 queries no banco! 😱
```

### ✅ **Solução (Com Eager Loading)**

Usar `.Include()` para trazer tudo de uma vez e fazer os cálculos em memória.

---

## Requisitos

Implemente um novo endpoint: `GET /api/Analytics/DemonRecommendations`

**Retorna:**
```json
{
    "recommendations": [
        {
            "demonId": "xyz",
            "demonName": "Asmodeus",
            "demonCategory": "Lust",
            "currentAssignments": 2,
            "recommendedSoul": {
                "soulId": "abc",
                "soulName": "John Doe",
                "soulLevel": "Inferior",
                "reasonsToAssign": [
                    "Demon least occupied",
                    "Level match",
                    "Category compatible"
                ]
            }
        },
        ...
    ],
    "insights": {
        "mostBurderedDemon": { "name": "...", "assignmentCount": 5 },
        "mostPersecutedSoul": { "name": "...", "persecutorCount": 3 },
        "averageAssignmentPerDemon": 1.8
    }
}
```

---

## Passos (Didáticos)

### 1️⃣ **Mapeie os dados necessários**
- Qual informação você precisa de cada entidade?
- Quantos níveis de relacionamentos? (Demon → Persecution → Soul → Cavern?)

### 2️⃣ **Identifique o N+1**
- Escreva um método **SEM** eager loading e conte quantas queries vão ao banco.
- Use os logs do Entity Framework para ver `Executed DbCommand`.

### 3️⃣ **Implemente com Eager Loading**
- Use `.Include()` para trazer Demons + Persecutions + Souls.
- Use `.ThenInclude()` se precisar de sub-relações (ex: Soul.Cavern).
- Faça os cálculos em memória com LINQ to Objects.

### 4️⃣ **Otimize com Select**
- Em vez de trazer a entidade inteira, use `.Select(d => new { ... })` para trazer só os campos necessários.
- Compara performance com `.Include()`.

### 5️⃣ **Teste com muitos dados**
- Crie 100+ Demons, 1000+ Souls, 500+ Persecutions.
- Compare tempo de resposta: N+1 vs Eager Loading vs Select.

---

## Dicas de Implementação

**Passo 1: Criar o DTO**
```csharp
public record DemonRecommendationDto(
    Guid DemonId,
    string DemonName,
    string CategoryName,
    int CurrentAssignments,
    SoulRecommendationDto? RecommendedSoul,
    List<string> ReasonsToAssign
);

public record SoulRecommendationDto(
    Guid SoulId,
    string SoulName,
    HellEnum SoulLevel
);
```

**Passo 2: Repository com Eager Loading**
```csharp
// Trazer Demons + Persecutions + Souls + Category + Cavern
var demons = await _context.Demons
    .Include(d => d.Category)
    .Include(d => d.Persecutions)
        .ThenInclude(p => p.Soul)
            .ThenInclude(s => s.Cavern)
    .ToListAsync();
```

**Passo 3: Lógica de Recomendação (em memória)**
```csharp
var demonsByOccupancy = demons
    .OrderBy(d => d.Persecutions.Count)
    .ToList();

var unassignedSouls = allSouls
    .Where(s => !demons.SelectMany(d => d.Persecutions)
        .Any(p => p.IdSoul == s.IdSoul))
    .ToList();

// Agora recomende o melhor par
```

---

## Conceitos a Explorar

- ✅ `.Include()` vs `.Select()`
- ✅ `.ThenInclude()` para relações nested
- ✅ LINQ to Objects vs LINQ to SQL
- ✅ Performance: comparar queries ao banco
- ✅ Memory usage: `.AsNoTracking()` vs tracked entities
- ✅ Caching: será que deveria cachear recomendações?

---

## Bônus (Se quiser mais desafio)

- Implemente **paginação** nas recomendações.
- Adicione **filtros** (ex: só demons da categoria "Wrath").
- Crie um endpoint `/api/Analytics/Insights` que retorna dados agregados.
- Use **specification pattern** para encapsular a lógica de query com eager loading.
- Implemente **caching com Redis** das recomendações.

---

## Resultado Esperado

Você vai entender:
1. **Por que eager loading importa** (diferença de 1 query vs 21 queries)
2. **Como estruturar queries complexas** com múltiplos `.Include()`
3. **Quando usar Select** para otimizar ainda mais
4. **Trade-offs** entre simplicidade e performance

**Boa sorte! 🔥**
