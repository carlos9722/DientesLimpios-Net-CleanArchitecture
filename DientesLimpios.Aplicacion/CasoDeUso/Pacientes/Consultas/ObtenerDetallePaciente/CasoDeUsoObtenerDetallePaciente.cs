
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerDetallePaciente
{
    /// <summary>
    /// Implementa el caso de uso encargado de obtener la información
    /// detallada de un paciente mediante su identificador único.
    /// </summary>
    /// <remarks>
    /// Consulta el paciente a través del repositorio y transforma la entidad
    /// del dominio en un DTO para entregar la información a la capa que realiza
    /// la solicitud.
    /// </remarks>
    public class CasoDeUsoObtenerDetallePaciente : IRequestHandler<ConsultaObtenerDetallePaciente, PacienteDetalleDTO>
    {
        /// <summary>
        /// Repositorio encargado de realizar las operaciones de consulta
        /// sobre los pacientes.
        /// </summary>
        private readonly IRepositorioPacientes repositorio;

        /// <summary>
        /// Inicializa una nueva instancia del caso de uso.
        /// </summary>
        /// <param name="repositorio">
        /// Implementación del contrato de acceso a los datos de pacientes.
        /// </param>
        public CasoDeUsoObtenerDetallePaciente(IRepositorioPacientes repositorio)
        {
            this.repositorio = repositorio;
        }

        /// <summary>
        /// Ejecuta la consulta para obtener el detalle de un paciente.
        /// </summary>
        /// <param name="request">
        /// Consulta que contiene el identificador único del paciente
        /// que se desea obtener.
        /// </param>
        /// <returns>
        /// Un objeto PacienteDetalleDTO con la información del paciente encontrado.
        /// </returns>
        /// <exception cref="ExcepcionNoEncontrado">
        /// Se lanza cuando no existe un paciente con el identificador proporcionado.
        /// </exception>
        public async Task<PacienteDetalleDTO> Handle(ConsultaObtenerDetallePaciente request)
        {
            // Consulta el repositorio para obtener el paciente utilizando su identificador.
            var paciente = await repositorio.ObtenerPorId(request.Id);

            // Verifica si el repositorio encontró un paciente.
            // Si no existe, lanza una excepción indicando que el recurso no fue encontrado.
            if (paciente is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            // Convierte la entidad del dominio en un DTO para entregar
            // únicamente la información correspondiente a la capa de aplicación.
            return paciente.ADto();
        }
    }
}
