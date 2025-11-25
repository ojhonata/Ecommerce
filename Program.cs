using System.Text;
using DotNetEnv;
using Ecommerce.Data;
using Ecommerce.Interface;
using Ecommerce.Mapping;
using Ecommerce.Repository;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Tenta carregar o .env se ele existir (para uso local)
try { Env.Load(); } catch { /* Ignora se não tiver arquivo .env no Render */ }

// Configurações de Upload
builder.Services.Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = 1024 * 1024 * 200; });
builder.WebHost.ConfigureKestrel(options => { options.Limits.MaxRequestBodySize = 200 * 1024 * 1024; });

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingDTO));
builder.Services.AddEndpointsApiExplorer();

// Swagger (Configurações omitidas para brevidade)
builder.Services.AddSwaggerGen(c => { /* ... */ });

// Configuração do DB e Serviços (Omitida para brevidade)
var mySqlString = Environment.GetEnvironmentVariable("MYSQL_URL");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mySqlString, ServerVersion.AutoDetect(mySqlString))
);

// ... Injeção de Dependências e JWT ...

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x => { /* ... */ });

// CORS (Liberado para o Render)
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure o HTTP request pipeline.

// Swagger visível na produção para facilitar seus testes
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// ----------------------------------------------------
// ✅ CORREÇÃO CRÍTICA AQUI: CORS DEVE VIR ANTES DE AUTH
// ----------------------------------------------------
app.UseCors("AllowAll"); // AQUI! Colocado ANTES de UseStaticFiles, UseAuthentication e UseAuthorization.

// Configurações de Static Files (mantidas)
var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".glb"] = "model/gltf-binary";
provider.Mappings[".gltf"] = "model/gltf+json";

var modelsPath = Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "models3D");
if (!Directory.Exists(modelsPath)) Directory.CreateDirectory(modelsPath);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(modelsPath),
    RequestPath = "/models3D",
    ContentTypeProvider = provider,
});

// Autenticação e Autorização devem vir depois do CORS
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Importante para o Render escutar a porta certa
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://*:{port}");