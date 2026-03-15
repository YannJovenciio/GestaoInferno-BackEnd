using System.Text.Json.Serialization;
using Inferno.src.Adapters.Models.ErrorHandlerMiddleware;
using Inferno.src.Adapters.Outbound.Persistence.HellTask;
using Inferno.src.Adapters.Outbound.Persistence.Repositories;
using Inferno.src.Adapters.Outbound.Persistence.Repositories.Auth;
using Inferno.src.Adapters.Outbound.Persistence.Repositories.Category;
using Inferno.src.Adapters.Outbound.Persistence.Repositories.Cavern;
using Inferno.src.Adapters.Outbound.Persistence.Repositories.Demon;
using Inferno.src.Adapters.Outbound.Persistence.Repositories.Persecution;
using Inferno.src.Adapters.Outbound.Persistence.Repositories.Sin;
using Inferno.src.Adapters.Outbound.Persistence.Repositories.Soul;
using Inferno.src.Adapters.Outbound.Workers;
using Inferno.src.Core.Application.Analytics.Demon;
using Inferno.src.Core.Application.Analytics.Soul;
using Inferno.src.Core.Application.Services.AuthJWT;
using Inferno.src.Core.Application.Services.OutBox;
using Inferno.src.Core.Application.UseCases.Auth;
using Inferno.src.Core.Application.UseCases.Category;
using Inferno.src.Core.Application.UseCases.Cavern;
using Inferno.src.Core.Application.UseCases.Demon;
using Inferno.src.Core.Application.UseCases.GetSinsBySeverity;
using Inferno.src.Core.Application.UseCases.HellTask;
using Inferno.src.Core.Application.UseCases.Sin;
using Inferno.src.Core.Application.UseCases.Soul;
using Inferno.src.Core.Domain.Event;
using Inferno.src.Core.Domain.Interfaces.Persecution;
using Inferno.src.Core.Domain.Interfaces.Repository.Category;
using Inferno.src.Core.Domain.Interfaces.Repository.Sin;
using Inferno.src.Core.Domain.Interfaces.Repository.Souls;
using Inferno.src.Core.Domain.Interfaces.UseCases;
using Inferno.src.Core.Domain.Interfaces.UseCases.Category;
using Inferno.src.Core.Domain.Interfaces.UseCases.Soul;
using Inferno.src.Infra;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
});

// Adicionar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
        }
    );
});

builder
    .Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

//Repositories
builder.Services.AddScoped<IDemonRepository, DemonRepository>();
builder.Services.AddScoped<ISoulRepository, SoulRepository>();
builder.Services.AddScoped<IPersecutionRepository, PersecutionRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISinRepository, SinRepository>();
builder.Services.AddScoped<ICavernRepository, CavernRepository>();
builder.Services.AddScoped<IDemonRecommendationQuery, DemonRecommendationQuery>();
builder.Services.AddScoped<IHellTaskRepository, HellTaskRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

//UseCases
builder.Services.AddScoped<IPersecutionUseCase, PersecutionUseCase>();
builder.Services.AddScoped<IDemonUseCase, DemonUseCase>();
builder.Services.AddScoped<ISoulUseCase, SoulUseCase>();
builder.Services.AddScoped<ICategoryUseCase, CategoryUseCase>();
builder.Services.AddScoped<ISinUseCase, SinUseCase>();
builder.Services.AddScoped<IGetSinsBySeverity, GetSinsBySeverity>();
builder.Services.AddScoped<IDemonRecomendationsUseCase, DemonRecomendationsUseCase>();
builder.Services.AddScoped<ISoulRecommendations, SoulRecommendations>();
builder.Services.AddScoped<ICavernUseCase, CavernUseCase>();
builder.Services.AddScoped<IAuthUseCase, AuthUseCase>();
builder.Services.AddScoped<IHellTaskUseCase, HellTaskUseCase>();

//Services
builder.Services.AddScoped<IEventPublisher, OutBoxEventPublisher>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IEventHandler<SinCreatedEvent>, SinCreatedHandler>();
builder.Services.AddHostedService<KeyRotationService>();
builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
            {
                //Console.WriteLine($"Received Token: {token}");
                //Console.WriteLine($"Token Issuer: {securityToken.Issuer}");
                //Console.WriteLine($"Key ID: {kid}");
                //Console.WriteLine($"Validate Lifetime: {parameters.ValidateLifetime}");

                var httpClient = new HttpClient();
                var jwks = httpClient
                    .GetStringAsync($"{builder.Configuration["Jwt:Issuer"]}/.well-known/jwks.json")
                    .Result;
                var keys = new JsonWebKeySet(jwks);
                return keys.Keys;
            },
        };
    });

builder
    .Services.AddAuthorizationBuilder()
    .SetFallbackPolicy(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());

//Hosted

builder.Services.AddHostedService<OutboxDispatcherService>();

//DbContext
builder.Services.AddDbContext<HellDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new()
        {
            Title = "Inferno API",
            Version = "v1",
            Description = "API para gerenciar o Inferno",
        }
    );
});

var app = builder.Build();

// Configurar pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inferno API v1");
        c.RoutePrefix = string.Empty;
    });
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Usar CORS
app.UseCors("AllowFrontend");

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Teste do banco de dados
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<HellDbContext>();
    Console.WriteLine($"Database path: {context.DbPath}");
}

await app.RunAsync();
