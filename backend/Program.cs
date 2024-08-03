using System.Text.Json.Serialization;
using backend.Data;
using backend.Interfaces;
using backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
    
builder.Services.AddScoped<ILogin, LoginService>();
builder.Services.AddScoped<IRegister, RegisterService>();
builder.Services.AddScoped<IChat, ChatService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
            .WithOrigins("http://localhost:3000") // Укажите разрешенные источники
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials(); // Разрешите креденциалы, если они необходимы
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Добавляем маршрутизацию
app.UseRouting();

// Использование авторизации (если есть)
app.UseAuthorization();

app.UseCors("CorsPolicy");

// Настройка конечных точек для контроллеров
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();