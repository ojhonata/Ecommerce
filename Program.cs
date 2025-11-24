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

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// --- AQUI ESTÁ A MÁGICA ---
// O Render vai injetar isso automaticamente se configurarmos lá no painel
var mySqlString = Environment.GetEnvironmentVariable("MYSQL_URL");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mySqlString, ServerVersion.AutoDetect(mySqlString))
);

// Injeção de Dependências
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddHttpClient<IViaCepService, ViaCepService>();

// JWT
var tokenSecret = Environment.GetEnvironmentVariable("TokenSecret");
var key = Encoding.ASCII.GetBytes(tokenSecret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

// CORS (Liberado para o Render)
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Swagger visível na produção para facilitar seus testes
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseHttpsRedirection();

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

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Importante para o Render escutar a porta certa
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://*:{port}");