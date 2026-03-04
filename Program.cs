using Microsoft.EntityFrameworkCore;
using ShopGestProjeto.Api.Data;
using ShopGestProjeto.Api.Services.Security;

var builder = WebApplication.CreateBuilder(args);

// 1. BANCO DE DADOS (Ajuste a string de conexão no appsettings.json para SQLite ou Postgres)
var connectionstring = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionstring)); // Mude para UseSqlite se não tiver um SQL Server na nuvem

builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 2. CORS (Perfeito como você fez)
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTudo",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// 3. MIDDLEWARES (Ordem recomendada)
app.UseHttpsRedirection();
app.UseCors("PermitirTudo");

// Deixe o Swagger fora do IF para conseguir visualizar na nuvem no início
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();