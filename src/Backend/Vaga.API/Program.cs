using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Vaga.Domain.Validators;
using Vaga.Infra.Context;
using Vaga.Infra.Interfaces;
using Vaga.Infra.Repositories;
using VagaClasse = Vaga.Domain.Entities.Vaga;

var builder = WebApplication.CreateBuilder(args);

// 1. Registra os Controllers + Mapeamento de Enums para String no JSON global
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. Configura o Banco de Dados em Memória
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("UniversoVagasDb"));

// 3. Injeção de Dependência
builder.Services.AddScoped<IVagaRepository, VagaRepository>();
builder.Services.AddScoped<IValidator<VagaClasse>, VagaValidator>();

// 4. Configuração do CORS profissional e segura para o Blazor
builder.Services.AddCors(options =>
{
    options.AddPolicy("BlazorApp", policy =>
        policy.WithOrigins("http://localhost:5162", "https://localhost:5162") 
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()); 
});

var app = builder.Build();

// 5. Swagger apenas para desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Vaga API v1");
    });
}

app.UseHttpsRedirection();

// 6. Ativa a política de CORS antes dos controllers
app.UseCors("BlazorApp");

app.UseAuthorization();
app.MapControllers();

app.Run();
