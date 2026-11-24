
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Citas.CrearCita
{
    /// <summary>
    /// Ejecuta el proceso de creación de una cita, validando la disponibilidad
    /// del dentista y gestionando su persistencia y notificación de confirmación.
    /// </summary>
    public class CasoDeUsoCrearCita : IRequestHandler<ComandoCrearCita, Guid>
    {
        /// <summary>
        /// Repositorio encargado de consultar y gestionar las citas.
        /// </summary>
        private readonly IRepositorioCitas repositorio;

        /// <summary>
        /// Unidad de trabajo encargada de confirmar o revertir
        /// las operaciones realizadas en la base de datos.
        /// </summary>
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        /// <summary>
        /// Servicio encargado de enviar las notificaciones relacionadas
        /// con la confirmación de la cita.
        /// </summary>
        private readonly IServicioNotificaciones servicioNotificaciones;

        /// <summary>
        /// Inicializa el caso de uso con las dependencias necesarias
        /// para gestionar la creación de citas.
        /// </summary>
        /// <param name="repositorio">
        /// Repositorio utilizado para consultar y registrar las citas.
        /// </param>
        /// <param name="unidadDeTrabajo">
        /// Servicio utilizado para confirmar o revertir los cambios en la base de datos.
        /// </param>
        /// <param name="servicioNotificaciones">
        /// Servicio encargado de enviar la confirmación de la cita.
        /// </param>
        public CasoDeUsoCrearCita(IRepositorioCitas repositorio, IUnidadDeTrabajo unidadDeTrabajo,
            IServicioNotificaciones servicioNotificaciones)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
            this.servicioNotificaciones = servicioNotificaciones;
        }

        /// <summary>
        /// Procesa la solicitud de creación de una cita, verificando que el
        /// dentista esté disponible y registrando la cita en la base de datos.
        /// </summary>
        /// <param name="request">
        /// Comando que contiene los datos necesarios para crear la cita.
        /// </param>
        /// <returns>
        /// Identificador único de la cita creada.
        /// </returns>
        /// <remarks>
        /// Si el dentista ya tiene una cita en el intervalo indicado,
        /// se genera una excepción de validación. Si ocurre un error
        /// durante la persistencia, se revierten los cambios realizados.
        /// </remarks>
        public async Task<Guid> Handle(ComandoCrearCita request)
        {
            var citaSeSolapa = await repositorio
                        .ExisteCitaSolapada(request.DentistaId, request.FechaInicio, request.FechaFin);

            if (citaSeSolapa)
            {
                throw new ExcepcionDeValidacion("El dentista ya tiene una cita en ese horario");
            }

            var intervaloDeTiempo = new IntervaloDeTiempo(request.FechaInicio, request.FechaFin);
            var cita = new Cita(request.PacienteId, request.DentistaId, request.ConsultorioId, intervaloDeTiempo);

            Guid? id = null;

            try
            {
                var respuesta = await repositorio.Agregar(cita);
                await unidadDeTrabajo.Persistir();
                id = respuesta.Id;
            }
            catch (Exception)
            {
                await unidadDeTrabajo.Reversar();
                throw;
            }

            var citaDB = await repositorio.ObtenerPorId(id.Value);
            var notificacionDTO = citaDB!.ADto();
            await servicioNotificaciones.EnviarConfirmacionCita(notificacionDTO);
            return id.Value;
        }
    }
}
