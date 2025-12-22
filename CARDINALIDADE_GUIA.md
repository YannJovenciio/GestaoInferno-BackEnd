# 📊 Guia de Cardinalidade de Relacionamentos

## O que é Cardinalidade?

Cardinalidade define **quantos registros de uma entidade podem se relacionar com quantos registros de outra entidade**.

---

## 🔤 Notação de Cardinalidade

```
[Mínimo]:[Máximo]
```

### Valores Possíveis:
| Símbolo | Significado | Exemplo |
|---------|------------|---------|
| `0` | Zero (opcional) | Pode não ter |
| `1` | Um (exatamente) | Deve ter um |
| `N` | Múltiplos (zero ou mais) | Pode ter vários |

---

## 📋 Tipos de Relacionamentos Comuns

### 1️⃣ Um-para-Um (1:1)
**Um registro de A se relaciona com exatamente um registro de B**

```
┌─────────┐          ┌─────────┐
│ Pessoa  │ 1─────1 │ CPF     │
└─────────┘          └─────────┘
```

- Uma pessoa tem um CPF
- Um CPF pertence a uma pessoa

**Cardinalidade:** `1:1`

**Código:**
```csharp
public class Pessoa
{
    public Guid IdPessoa { get; set; }
    public virtual CPF CPF { get; set; }
}

public class CPF
{
    public Guid IdCPF { get; set; }
    public Guid IdPessoa { get; set; }
    public virtual Pessoa Pessoa { get; set; }
}

// Configuração
builder.HasOne(c => c.Pessoa)
    .WithOne(p => p.CPF)
    .HasForeignKey<CPF>(c => c.IdPessoa);
```

---

### 2️⃣ Um-para-Muitos (1:N)
**Um registro de A se relaciona com múltiplos registros de B**

```
┌──────────┐          ┌──────────┐
│ Category │ 1─────N │  Demon   │
└──────────┘          └──────────┘
```

- Uma categoria tem vários demônios
- Um demônio pertence a uma categoria

**Cardinalidade:** `1:N`

**Código:**
```csharp
public class Category
{
    public Guid IdCategoria { get; set; }
    public string NomeCategoria { get; set; }
    public virtual ICollection<Demon> Demons { get; set; } = new List<Demon>();
}

public class Demon
{
    public Guid IdDemon { get; set; }
    public Guid? CategoryId { get; set; }  // Foreign Key
    public string DemonName { get; set; }
    public virtual Category Category { get; set; }
}

// Configuração
builder.HasOne(d => d.Category)
    .WithMany(c => c.Demons)
    .HasForeignKey(d => d.CategoryId);
```

---

### 3️⃣ Muitos-para-Muitos (N:N)
**Múltiplos registros de A se relacionam com múltiplos registros de B**

```
┌────────┐          ┌──────────┐          ┌────────┐
│ Demon  │ N─────N │ Skillset │
└────────┘          └──────────┘          
```

- Um demônio pode ter múltiplas habilidades
- Uma habilidade pode pertencer a múltiplos demônios

**Cardinalidade:** `N:N`

**Requer Join Entity (tabela de junção)**

**Código:**
```csharp
public class Demon
{
    public Guid IdDemon { get; set; }
    public string DemonName { get; set; }
    public virtual ICollection<DemonSkillset> Skills { get; set; } = new List<DemonSkillset>();
}

public class Skillset
{
    public Guid IdSkillset { get; set; }
    public string SkillName { get; set; }
    public virtual ICollection<DemonSkillset> Demons { get; set; } = new List<DemonSkillset>();
}

public class DemonSkillset  // ← Join Entity
{
    public Guid IdDemon { get; set; }
    public Guid IdSkillset { get; set; }
    public virtual Demon Demon { get; set; }
    public virtual Skillset Skillset { get; set; }
}

// Configuração
builder.HasKey(ds => new { ds.IdDemon, ds.IdSkillset });

builder.HasOne(ds => ds.Demon)
    .WithMany(d => d.Skills)
    .HasForeignKey(ds => ds.IdDemon);

builder.HasOne(ds => ds.Skillset)
    .WithMany(s => s.Demons)
    .HasForeignKey(ds => ds.IdSkillset);
```

---

## 🎯 Sua Situação: Category-Demon

### Você disse:
> Uma categoria pode se relacionar com **no mínimo zero** e **no máximo N**

### Traduzindo:
```
┌──────────┐     0..N     ┌─────────┐
│ Category │ ───────────── │ Demon   │
└──────────┘               └─────────┘
```

- **Cardinalidade:** `0:N` (Zero-ou-Muitos)
- Uma categoria pode ter **nenhum demônio** (0)
- Uma categoria pode ter **vários demônios** (N)
- Um demônio pertence a **uma categoria** (1)
- Um demônio pode **não ter categoria** (0) ← opcional

### Relacionamento Completo:
```
Category (0:N) ←→ Demon

Category: 1 pode ter 0 ou muitos Demons
Demon: 0 ou 1 pode ter Category
```

---

## 🛠️ Implementação para Category-Demon (0:N)

### Category.cs
```csharp
public class Category
{
    public Guid IdCategoria { get; set; }
    public required string NomeCategoria { get; set; }
    
    // Uma categoria tem muitos demônios (0 a N)
    public virtual ICollection<Demon> Demons { get; set; } = new List<Demon>();

    public Category() { }
}
```

### Demon.cs
```csharp
public class Demon
{
    public Guid IdDemon { get; set; }
    
    // Foreign Key - opcional (pode ser null)
    public Guid? CategoryId { get; set; }
    
    public string DemonName { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Um demônio pode ter 0 ou 1 categoria
    public virtual Category Category { get; set; }
    
    // Relacionamento N:N com Soul
    public virtual ICollection<Persecution> Persecutions { get; set; } = new List<Persecution>();

    public Demon() { }

    public Demon(string demonName, Category category = null)
    {
        DemonName = demonName;
        Category = category;
    }
}
```

### CategoryConfiguration.cs
```csharp
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.IdCategoria);
        builder.Property(c => c.NomeCategoria).IsRequired();
        
        // Uma categoria tem muitos demônios
        // Um demônio pertence a uma categoria (opcional)
        builder
            .HasMany(c => c.Demons)
            .WithOne(d => d.Category)
            .HasForeignKey(d => d.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);  // Se categoria for deletada, demon.CategoryId vira null
    }
}
```

### DemonConfiguration.cs
```csharp
public class DemonConfiguration : IEntityTypeConfiguration<Demon>
{
    public void Configure(EntityTypeBuilder<Demon> builder)
    {
        builder.HasKey(d => d.IdDemon);
        builder.Property(d => d.DemonName).IsRequired();
        builder.Property(d => d.CategoryId).IsRequired(false);  // ← Opcional!
        
        // O relacionamento com Category é configurado em CategoryConfiguration
        // (pode estar aqui também, mas recomenda-se um único lugar)
    }
}
```

---

## 📌 Checklist Rápido

Quando você vê um relacionamento, pergunte:

- [ ] **Um A pode ter quantos B?** (0, 1, ou N?)
- [ ] **Um B pode ter quantos A?** (0, 1, ou N?)
- [ ] **É obrigatório ou opcional?**
- [ ] **Se um for deletado, o outro também deve ser?** (cascade delete)

---

## 📐 Tabela de Decisão

| Pergunta | Resposta | Tipo | Implementação |
|----------|----------|------|----------------|
| Um A com um B? | Sim, sempre | 1:1 | `HasOne().WithOne()` |
| Um A com N B? | Sim | 1:N | `HasMany().WithOne()` |
| Um A com 0 ou N B? | Sim | 0:N | `HasMany().WithOne()` + `IsRequired(false)` |
| N A com N B? | Sim | N:N | Precisa Join Entity |

---

## ✅ Seu Caso Final

**Category ← (0:N) → Demon**

```csharp
// Category: "Eu posso ter de 0 a N demônios"
public virtual ICollection<Demon> Demons { get; set; }

// Demon: "Eu posso ter 0 ou 1 categoria"
public Guid? CategoryId { get; set; }  // ? = nullable
public virtual Category Category { get; set; }
```

**Pronto!** Agora é 0:N conforme você especificou. ✨
