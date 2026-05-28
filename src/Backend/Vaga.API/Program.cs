using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Vaga.Domain.Validators;
using Vaga.Infra.Context;
using Vaga.Infra.Interfaces;
using Vaga.Infra.Repositories;
using VagaClasse = Vaga.Domain.Entities.Vaga;

var builder = WebApplication.CreateBuilder(args);

// 1. Registra os Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Configura o Banco de Dados em Memória (Para testes rápidos)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("UniversoVagasDb"));

// 3. Injeção de Dependência das Regras de Negócio e Banco
builder.Services.AddScoped<IVagaRepository, VagaRepository>();
builder.Services.AddScoped<IValidator<VagaClasse>, VagaValidator>();

// 4. Configuração do CORS (Evita o bloqueio do navegador)
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorApp", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

var app = builder.Build();

// 5. Swagger apenas para desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 6. Ativa a política de CORS antes dos controllers
app.UseCors("BlazorApp");

app.UseAuthorization();
app.MapControllers();

app.Run();
