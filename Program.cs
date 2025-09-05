using DotNetEnv;
using Ecommerce.Data;
using Ecommerce.Interface;
using Ecommerce.Repository;
using Ecommerce.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Env.Load(); // Carregar variáveis do .env

var pgSqlString = Environment.GetEnvironmentVariable("POSTGRES_URL");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(pgSqlString));

// var mySqlString = Environment.GetEnvironmentVariable("MYSQL_URL");
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseMySql(mySqlString, ServerVersion.AutoDetect(mySqlString)));

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();

builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
builder.Services.AddScoped<IMarcaService, MarcaService>();

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();