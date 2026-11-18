
using DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.ActualizarPaciente;
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Comandos.ActualizarPaciente
{
    /// <summary>
    /// Implementa el caso de uso encargado de actualizar la información
    /// de un paciente existente en el sistema.
    /// </summary>
    /// <remarks>
    /// Recupera la entidad del dominio, aplica las modificaciones mediante
    /// sus métodos de negocio y persiste los cambios utilizando el repositorio
    /// y la unidad de trabajo.
    /// </remarks>
    public class CasoDeUsoActualizarPaciente : IRequestHandler<ComandoActualizarPaciente>
    {
        /// <summary>
        /// Repositorio encargado de consultar y actualizar la información
        /// de los pacientes.
        /// </summary>
        private readonly IRepositorioPacientes repositorio;

        /// <summary>
        /// Unidad de trabajo encargada de controlar la persistencia
        /// de los cambios realizados en la operación.
        /// </summary>
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        /// <summary>
        /// Inicializa una nueva instancia del caso de uso de actualización de pacientes.
        /// </summary>
        /// <param name="repositorio">
        /// Contrato de acceso a las operaciones de consulta y actualización de pacientes.
        /// </param>
        /// <param name="unidadDeTrabajo">
        /// Contrato encargado de confirmar o revertir los cambios de persistencia.
        /// </param>
        public CasoDeUsoActualizarPaciente(IRepositorioPacientes repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }

        /// <summary>
        /// Ejecuta el proceso de actualización de un paciente.
        /// </summary>
        /// <param name="request">
        /// Comando que contiene el identificador del paciente y los nuevos datos
        /// que se desean actualizar.
        /// </param>
        /// <returns>
        /// Una tarea que representa la ejecución asíncrona del proceso de actualización.
        /// </returns>
        /// <exception cref="ExcepcionNoEncontrado">
        /// Se lanza cuando no existe un paciente con el identificador proporcionado.
        /// </exception>
        public async Task Handle(ComandoActualizarPaciente request)
        {
            // Busca el paciente en el repositorio utilizando el identificador recibido.
            var paciente = await repositorio.ObtenerPorId(request.Id);

            // Verifica que el paciente exista antes de intentar modificarlo.
            if (paciente is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            // Actualiza el nombre utilizando el método de la entidad,
            // que se encarga de aplicar las reglas de negocio correspondientes.
            paciente.ActualizarNombre(request.Nombre);

            // Construye el objeto de valor Email a partir del correo recibido.
            var email = new Email(request.Email);

            // Actualiza el correo electrónico mediante la entidad,
            // respetando las validaciones definidas en el dominio.
            paciente.ActualizarEmail(email);

            try
            {
                // Solicita al repositorio actualizar la información del paciente.
                await repositorio.Actualizar(paciente);

                // Confirma la persistencia de los cambios realizados.
                await unidadDeTrabajo.Persistir();
            }
            catch (Exception)
            {
                // Revierte los cambios pendientes cuando ocurre un error
                // durante el proceso de actualización o persistencia.
                await unidadDeTrabajo.Reversar();

                // Propaga nuevamente la excepción para que pueda ser
                // gestionada por las capas superiores de la aplicación.
                throw;
            }
        }
    }
}
