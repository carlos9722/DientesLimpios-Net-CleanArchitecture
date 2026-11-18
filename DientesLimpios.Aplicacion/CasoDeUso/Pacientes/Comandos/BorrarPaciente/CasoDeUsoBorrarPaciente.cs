
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Comandos.BorrarPaciente
{
    /// <summary>
    /// Implementa el caso de uso encargado de eliminar un paciente
    /// existente del sistema.
    /// </summary>
    /// <remarks>
    /// Verifica la existencia del paciente mediante el repositorio
    /// y coordina la eliminación y persistencia de los cambios
    /// utilizando la unidad de trabajo.
    /// </remarks>
    public class CasoDeUsoBorrarPaciente : IRequestHandler<ComandoBorrarPaciente>
    {
        /// <summary>
        /// Repositorio encargado de consultar y realizar operaciones
        /// sobre los pacientes.
        /// </summary>
        private readonly IRepositorioPacientes repositorio;

        /// <summary>
        /// Unidad de trabajo encargada de confirmar o revertir
        /// los cambios realizados durante la operación.
        /// </summary>
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        /// <summary>
        /// Inicializa una nueva instancia del caso de uso de eliminación de pacientes.
        /// </summary>
        /// <param name="repositorio">
        /// Contrato que proporciona las operaciones de acceso a los pacientes.
        /// </param>
        /// <param name="unidadDeTrabajo">
        /// Contrato encargado de gestionar la persistencia de los cambios.
        /// </param>
        public CasoDeUsoBorrarPaciente(IRepositorioPacientes repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }

        /// <summary>
        /// Ejecuta el proceso de eliminación de un paciente.
        /// </summary>
        /// <param name="request">
        /// Comando que contiene el identificador único del paciente
        /// que se desea eliminar.
        /// </param>
        /// <returns>
        /// Una tarea que representa la ejecución asíncrona del proceso de eliminación.
        /// </returns>
        /// <exception cref="ExcepcionNoEncontrado">
        /// Se lanza cuando no existe un paciente con el identificador proporcionado.
        /// </exception>
        public async Task Handle(ComandoBorrarPaciente request)
        {
            // Busca el paciente en el repositorio utilizando el identificador recibido.
            var paciente = await repositorio.ObtenerPorId(request.Id);

            // Verifica que el paciente exista antes de intentar eliminarlo.
            if (paciente is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            try
            {
                // Solicita al repositorio eliminar el paciente encontrado.
                await repositorio.Borrar(paciente);

                // Confirma la persistencia de los cambios realizados.
                await unidadDeTrabajo.Persistir();
            }
            catch (Exception)
            {
                // Intenta revertir los cambios pendientes cuando ocurre un error
                // durante el proceso de eliminación o persistencia.
                await unidadDeTrabajo.Reversar();

                // Propaga nuevamente la excepción para que pueda ser
                // gestionada por las capas superiores de la aplicación.
                throw;
            }
        }
    }
}
