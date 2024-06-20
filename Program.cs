using Microsoft.EntityFrameworkCore;
using ProjectTrackerAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuration de la lecture du fichier appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });

// Ajout du DbContext avec la chaîne de connexion à MariaDB et spécification de la version du serveur
builder.Services.AddDbContext<ProjectTrackerContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var serverVersion = new MariaDbServerVersion(new Version(10, 4, 28)); // Version de MariaDB
    options.UseMySql(connectionString, serverVersion);
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
