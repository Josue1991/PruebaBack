using Microsoft.EntityFrameworkCore;
using Servicios.impl;
using DataModel;
using Servicios;

var builder = WebApplication.CreateBuilder(args);

// Configurar el contexto de datos con reintento en caso de errores transitorios
builder.Services.AddDbContext<tiendaEntities1>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlServerOptions => sqlServerOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Número máximo de reintentos
            maxRetryDelay: TimeSpan.FromSeconds(10), // Retraso máximo entre reintentos
            errorNumbersToAdd: null // Puedes especificar números de error SQL específicos si es necesario
        )
    )
);

// Configurar los servicios de la aplicación
builder.Services.AddScoped<IEventoService, EventoService>();


// Añade la política CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()  
               .AllowAnyMethod()   
               .AllowAnyHeader();  
    });
});

// Agregar controladores y otros servicios necesarios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el pipeline de la aplicación
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
