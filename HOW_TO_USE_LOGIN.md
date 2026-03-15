## Como Usar o Login JWT

### 1. Configuração em `appsettings.json`

Adicione a seção `Jwt` com suas configurações:

```json
{
  "Jwt": {
    "Issuer": "https://seu-app.com",
    "Audience": "https://seu-cliente.com",
    "SecretKey": "sua-chave-super-secreta-com-256-bits-minimo",
    "ExpirationMinutes": 60,
    "ValidateIssuer": true,
    "ValidateAudience": true,
    "ValidateLifetime": true,
    "RequireHttpsMetadata": true
  }
}
```

### 2. POST Request - Fazer Login

**Endpoint:**
```
POST /api/auth/login
```

**Headers:**
```
Content-Type: application/json
```

**Body:**
```json
{
  "clientId": "seu-client-id",
  "email": "usuario@example.com",
  "password": "senha123"
}
```

**Response (sucesso - 200):**
```json
{
  "token": "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

**Response (erro - 401):**
```json
{
  "message": "Invalid credentials."
}
```

### 3. Usar o Token em Requisições Subsequentes

**Headers:**
```
Authorization: Bearer eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9...
```

### 4. Fluxo Arquitetural

```
HTTP POST /api/auth/login
         ↓
    AuthController
         ↓ (ILoginUseCase)
    LoginUseCase
         ├─→ IAuthRepository.GetClientByIdAsync()
         ├─→ IAuthRepository.GetDemonByEmailAsync()
         ├─→ BCrypt verify password
         ├─→ IAuthRepository.GetActiveSigningKeyAsync()
         └─→ ITokenService.GenerateToken()
         ↓
    HTTP 200 OK + Token
```

### 5. Exemplo com cURL

```bash
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "clientId": "client-123",
    "email": "user@example.com",
    "password": "password123"
  }'
```

### 6. Requisitos Prévios

1. **Banco de dados** deve ter:
   - Uma entidade `Client` com `ClientId` e `ClientURL`
   - Uma entidade `Demon` (usuário) com `DemonEmail`, `Password` (hashado com BCrypt)
   - Uma entidade `SigninKey` com a chave privada RSA (em Base64) e `IsActive = true`
   - Relacionamento entre `Demon` e `Role` via `DemonRoles`

2. **BCrypt** instalado no projeto:
   ```bash
   dotnet add package BCrypt.Net-Next
   ```

3. **Services registrados em Program.cs:**
   ```csharp
   builder.Services.AddScoped<IAuthRepository, AuthRepository>();
   builder.Services.AddScoped<ITokenService, TokenService>();
   builder.Services.AddScoped<ILoginUseCase, LoginUseCase>();
   builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));
   builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<JwtOptions>>().Value);
   ```

### 7. Estrutura de Responsabilidades

| Componente | Responsabilidade |
|-----------|-----------------|
| **AuthController** | Recebe requisição HTTP, valida ModelState, chama UseCase |
| **LoginUseCase** | Orquestra: valida cliente, usuario, senha e gera token |
| **AuthRepository** | Acessa BD (EF Core): clientes, demônios, chaves de assinatura |
| **TokenService** | Gera JWT: importa chave RSA, cria claims, assina token |
| **JwtOptions** | POCO com config: Issuer, Audience, ExpirationMinutes |

### 8. Tratamento de Erros

- **401 Unauthorized**: Cliente ou credenciais inválidas
- **500 Internal Server Error**: Chave de assinatura não encontrada or erro ao gerar token
- **400 Bad Request**: Dados de entrada inválidos (email, password, clientId)
