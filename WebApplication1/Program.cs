using Azure.Storage.Blobs;
using BlobStorageAPI.Interfaces;
using BlobStorageAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ?? Подключение к Azure Blob Storage
string connectionString = builder.Configuration.GetConnectionString("AzureBlobConnection");
builder.Services.AddSingleton(x => new BlobServiceClient(connectionString));
builder.Services.AddScoped<IBlobRepository, BlobRepository>();

// ?? Добавление базовых сервисов
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ?? Настройка CORS для Static Web App
var frontendUrl = "https://kind-sky-0ebf70103.6.azurestaticapps.net";

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowStaticWebApp", policy =>
    {
        policy.WithOrigins(frontendUrl) // ?? Указываем твой фронт
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// ?? Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// ?? Активируем CORS
app.UseCors("AllowStaticWebApp");

app.UseAuthorization();
app.MapControllers();

app.Run();
