
using DientesLimpios.API.Middlewares;
using DientesLimpios.Aplicacion;
using DientesLimpios.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Registra los servicios necesarios para exponer los controladores HTTP.
builder.Services.AddControllers();

// Registra las dependencias de la capa de aplicación y persistencia.
builder.Services.AgregarServiciosDeAplicacion();
builder.Services.AgregarServiciosDePersistencia();

var app = builder.Build();

// Configura el pipeline que procesa las solicitudes HTTP.

// Captura y gestiona las excepciones no controladas de la aplicación.
app.UseManejadorExcepciones();

// Redirige las solicitudes HTTP a HTTPS.
app.UseHttpsRedirection();

// Aplica las políticas de autorización configuradas.
app.UseAuthorization();

// Asocia las rutas de los controladores con sus endpoints HTTP.
app.MapControllers();

// Inicia la aplicación y comienza a escuchar solicitudes.
app.Run();
