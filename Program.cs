using Inferno.src.Core.Application.UseCases.Demon;
using Inferno.src.Core.Domain.Entities;
using Inferno.src.Core.Domain.Interfaces;
using Inferno.src.Core.Domain.Interfaces.UseCases;
using Inferno.src.Core.Domain.Interfaces.UseCases.Demon;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços
builder.Services.AddScoped<IDemonRepository, DemonRepository>();
builder.Services.AddScoped<IPersecutionUseCase, PersecutionUseCase>();
builder.Services.AddScoped<IDemonUseCase, DemonUseCase>();
builder.Services.AddControllers();
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
app.MapControllers();

// Teste do banco de dados
using (var scope = app.Services.CreateScope())
{
    var context =
        scope.ServiceProvider.GetRequiredService<Inferno.src.Adapters.Outbound.Persistence.HellDbContext>();
    Console.WriteLine($"Database path: {context.DbPath}");
}

app.Run();
