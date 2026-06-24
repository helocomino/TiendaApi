using Microsoft.EntityFrameworkCore;
using TiendaApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurar Conexión a PostgreSQL
// REEMPLAZA TU BLOQUE ANTERIOR POR ESTE:
// 1. Pega aquí adentro tu enlace original copiado de Supabase
// 1. Pega aquí tu enlace original directo de Supabase
var connectionUri = new Uri("postgresql://postgres.mpqrhfgvvvqualwuscoa:1GustavotkGuss@db.mpqrhfgvvvqualwuscoa.supabase.co:5432/postgres");

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
builder.Services.AddEndpointsApiExplorer(); // Puedes dejar esta línea, no da problemas

var app = builder.Build();

// SE ELIMINÓ EL BLOQUE DE SWAGGER QUE ESTABA AQUÍ

app.UseCors("PermitirTodo");
app.UseAuthorization();
app.UseStaticFiles(); // <-- Agrega esto para que sirva el archivo index.html
app.MapControllers();

app.Run();