using System.Text.Json.Serialization;
using Inferno.src.Adapters.Models.ErrorHandlerMiddleware;
using Inferno.src.Adapters.Outbound.Persistence.Repositories.Category;
using Inferno.src.Adapters.Outbound.Persistence.Repositories.Persecution;
using Inferno.src.Adapters.Outbound.Persistence.Repositories.Sin;
using Inferno.src.Adapters.Outbound.Persistence.Repositories.Soul;
using Inferno.src.Adapters.Outbound.Workers;
using Inferno.src.Core.Application.UseCases.Category;
using Inferno.src.Core.Application.UseCases.Demon;
using Inferno.src.Core.Application.UseCases.GetSinsBySeverity;
using Inferno.src.Core.Application.UseCases.Services;
using Inferno.src.Core.Application.UseCases.Sin;
using Inferno.src.Core.Application.UseCases.Soul;
using Inferno.src.Core.Domain.Event;
using Inferno.src.Core.Domain.Interfaces;
using Inferno.src.Core.Domain.Interfaces.Persecution;
using Inferno.src.Core.Domain.Interfaces.Repository.Category;
using Inferno.src.Core.Domain.Interfaces.Repository.Sin;
using Inferno.src.Core.Domain.Interfaces.Repository.Souls;
using Inferno.src.Core.Domain.Interfaces.UseCases;
using Inferno.src.Core.Domain.Interfaces.UseCases.Category;
using Inferno.src.Core.Domain.Interfaces.UseCases.Demon;
using Inferno.src.Core.Domain.Interfaces.UseCases.Soul;

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

//UseCases
builder.Services.AddScoped<IPersecutionUseCase, PersecutionUseCase>();
builder.Services.AddScoped<IDemonUseCase, DemonUseCase>();
builder.Services.AddScoped<ISoulUseCase, SoulUseCase>();
builder.Services.AddScoped<ICategoryUseCase, CategoryUseCase>();
builder.Services.AddScoped<ISinUseCase, SinUseCase>();
builder.Services.AddScoped<IGetSinsBySeverity, GetSinsBySeverity>();

//Services
builder.Services.AddScoped<IEventPublisher, OutBoxEventPublisher>();
builder.Services.AddScoped<IEventHandler<SinCreatedEvent>, SinCreatedHandler>();

//Hosted

builder.Services.AddHostedService<OutboxDispatcherService>();

//DbContext
builder.Services.AddDbContext<Inferno.src.Adapters.Outbound.Persistence.HellDbContext>();
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

app.MapControllers();

// Teste do banco de dados
using (var scope = app.Services.CreateScope())
{
    var context =
        scope.ServiceProvider.GetRequiredService<Inferno.src.Adapters.Outbound.Persistence.HellDbContext>();
    Console.WriteLine($"Database path: {context.DbPath}");
}

app.Run();
