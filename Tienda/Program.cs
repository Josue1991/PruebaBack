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
            maxRetryCount: 5, // N�mero m�ximo de reintentos
            maxRetryDelay: TimeSpan.FromSeconds(10), // Retraso m�ximo entre reintentos
            errorNumbersToAdd: null // Puedes especificar n�meros de error SQL espec�ficos si es necesario
        )
    )
);

// Configurar los servicios de la aplicaci�n
builder.Services.AddScoped<IEventoService, EventoService>();


// A�ade la pol�tica CORS
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

// Configurar el pipeline de la aplicaci�n
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
