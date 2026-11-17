using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Comandos.CrearConsultorio;
using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerDetalleConsultorio;
using DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerListadoConsultorios;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio;
using DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion
{
    /// <summary>
    /// Contiene los métodos de extensión necesarios para registrar
    /// los servicios y las dependencias de la capa de aplicación.
    /// </summary>
    public static class RegistroDeServiciosDeAplicacion
    {
        /// <summary>
        /// Registra el mediador y los manejadores de solicitudes de los casos de uso
        /// en el contenedor de inyección de dependencias.
        /// </summary>
        /// <param name="services">
        /// Contenedor de servicios donde se registran las dependencias de la aplicación.
        /// </param>
        /// <returns>
        /// La colección de servicios actualizada, permitiendo encadenar otros registros.
        /// </returns>
        public static IServiceCollection AgregarServiciosDeAplicacion(
            this IServiceCollection services)
        {
            // Registra el mediador con ciclo de vida transitorio.
            // Esto permite crear una nueva instancia cada vez que sea solicitada.
            services.AddTransient<IMediator, MediadorSimple>();

            // Busca automáticamente los manejadores de solicitudes en el ensamblado
            // donde se encuentra definido IMediator y los registra en el contenedor.
            services.Scan(scan =>
                scan.FromAssembliesOf(typeof(IMediator))

                    // Registra los manejadores que procesan solicitudes sin devolver resultados.
                    .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()

                    // Registra los manejadores que procesan solicitudes y devuelven resultados.
                    .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());


            /*
            // Registra el manejador encargado de procesar las solicitudes de creación de consultorios.
            services.AddScoped<IRequestHandler<ComandoCrearConsultorio, Guid>,
                               CasoDeUsoCrearConsultorio>();

            // Registra el manejador encargado de obtener el detalle de un consultorio.
            services.AddScoped<IRequestHandler<ConsultaObtenerDetalleConsultorio, ConsultorioDetalleDTO>,
                               CasoDeUsoObtenerDetalleConsultorio>();

            // Registra el manejador encargado de obtener el listado de consultorios.
            services.AddScoped<IRequestHandler<ConsultaObtenerListadoConsultorios,
                                List<ConsultorioListadoDTO>>, CasoDeUsoObtenerListadoConsultorios>();

            // Registra el manejador encargado de procesar las solicitudes de actualización de consultorios.
            services.AddScoped<IRequestHandler<ComandoActualizarConsultorio>, CasoDeUsoActualizarConsultorio>();

            // Registra el manejador encargado de procesar las solicitudes de eliminación de consultorios.
            services.AddScoped<IRequestHandler<ComandoBorrarConsultorio>, CasoDeUsoBorrarConsultorio>();
            */

            // Retorna la colección de servicios con todas las dependencias registradas.
            return services;
        }
    }
}
