using Microsoft.EntityFrameworkCore;
using TiendaApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar Conexión a PostgreSQL con la base de datos de Render
// PEGA AQUÍ TU EXTERNAL DATABASE URL DE RENDER DIRECTAMENTE:
var connectionUri = new Uri("postgresql://tienda_db_0pv7_user:q7DYCgyAUtmIO2e1YFFHoDIaQqhQ6OfY@dpg-d8u0m7vlk1mc73c665k0-a.oregon-postgres.render.com/tienda_db_0pv7");

// 2. Extraemos los componentes automáticamente para armar una cadena limpia
var dbUser = connectionUri.UserInfo.Split(':')[0];
var dbPassword = connectionUri.UserInfo.Split(':')[1];
var dbHost = connectionUri.Host;
var dbPort = connectionUri.Port;
var dbName = connectionUri.AbsolutePath.TrimStart('/');

var connectionString = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword};";

// 3. Conectamos Entity Framework con la cadena formateada
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// 2. Habilitar CORS para conectar con el Frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseCors("PermitirTodo");
app.UseAuthorization();
app.UseStaticFiles(); 
app.MapControllers();

app.Run();