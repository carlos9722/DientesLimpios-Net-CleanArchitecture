
using DientesLimpios.API.Jobs;
using DientesLimpios.API.Middlewares;
using DientesLimpios.Aplicacion;
using DientesLimpios.Identidad;
using DientesLimpios.Identidad.Modelos;
using DientesLimpios.Infraestructura;
using DientesLimpios.Persistencia;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Registra los controladores y establece un filtro de autorización global para permitir únicamente el acceso a los usuarios que cumplan la política "esadmin".
builder.Services.AddControllers(opciones =>
{
    opciones.Filters.Add(new AuthorizeFilter("esadmin"));
});

// Registra los servicios de las diferentes capas de la aplicación mediante inyección de dependencias.
builder.Services.AgregarServiciosDeAplicacion();
builder.Services.AgregarServiciosDePersistencia();
builder.Services.AgregarServiciosDeInfraestructura();

// Registra el servicio en segundo plano encargado de ejecutar las tareas de recordatorio de citas.
builder.Services.AddHostedService<RecordatorioCitasJob>();

// Registra los servicios necesarios para gestionar la autenticación y la identidad de los usuarios.
builder.Services.AgregarServiciosDeIdentidad();

var app = builder.Build();

// Configura el pipeline HTTP, definiendo los componentes que procesan las solicitudes entrantes.

// Expone los endpoints de identidad para gestionar operaciones como el registro y la autenticación de usuarios.
app.MapIdentityApi<Usuario>();

// Intercepta las excepciones no controladas y las procesa de forma centralizada.
app.UseManejadorExcepciones();

// Redirige automáticamente las solicitudes HTTP hacia HTTPS para establecer una conexión segura.
app.UseHttpsRedirection();

// Aplica las políticas de autorización para controlar el acceso a los recursos protegidos.
app.UseAuthorization();

// Registra los endpoints de los controladores para que puedan recibir solicitudes HTTP.
app.MapControllers();

// Inicia la aplicación y comienza a escuchar las solicitudes HTTP entrantes.
app.Run();
