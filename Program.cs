using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Produtos.Repositories;
using Produtos.Data;
using Produtos.Services;
using Produtos.Mappings;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Carrega variáveis de ambiente do arquivo .env se estiver em desenvolvimento
if (builder.Environment.IsDevelopment())
{
    Env.Load();
}

// Carrega variáveis de ambiente do sistema operacional
builder.Configuration.AddEnvironmentVariables();

// Carrega configuração do appsettings.{Environment}.json
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Products API",
        Version = "v1",
        Description = "API for managing product registrations",
    });
    c.EnableAnnotations();
});

// Configura AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Pega a string de conexão a partir da variável de ambiente (.env ou OS)
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_URI");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("A string de conexão não foi configurada. Defina a variável de ambiente 'DB_CONNECTION_URI'.");
}

// Configura o DbContext
builder.Services.AddDbContext<ProdutoContext>(options =>
    options.UseSqlServer(connectionString));

// Registra o repositório
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

// Registra o serviço
builder.Services.AddScoped<ProdutoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
