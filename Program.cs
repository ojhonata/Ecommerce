using DotNetEnv;
using Ecommerce.Data;
using Ecommerce.Interface;
using Ecommerce.Mapping;
using Ecommerce.Repository;
using Ecommerce.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the   container.

builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(MappingDTO));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Env.Load(); // Carregar variáveis do .env

// var pgSqlString = Environment.GetEnvironmentVariable("POSTGRES_URL");
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseNpgsql(pgSqlString));

var mySqlString = Environment.GetEnvironmentVariable("MYSQL_URL");  
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(mySqlString, ServerVersion.AutoDetect(mySqlString))
);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IBrandService, BrandService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddScoped<IPaymentService, PaymentService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "AllowAll",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader();
        }
    );
});

var app = builder.Build();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
