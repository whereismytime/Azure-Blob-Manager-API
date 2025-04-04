using Azure.Storage.Blobs;
using BlobStorageAPI.Interfaces;
using BlobStorageAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Azure Blob Storage connection
string connectionString = builder.Configuration.GetConnectionString("AzureBlobConnection");
builder.Services.AddSingleton(x => new BlobServiceClient(connectionString));
builder.Services.AddScoped<IBlobRepository, BlobRepository>();

var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
