using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerListadoPacientes
{
    /// <summary>
    /// Implementa el caso de uso encargado de obtener el listado
    /// de pacientes registrados en el sistema.
    /// </summary>
    /// <remarks>
    /// Consulta los pacientes mediante el repositorio y transforma
    /// las entidades del dominio en objetos PacienteListadoDTO
    /// para entregar únicamente la información necesaria al consumidor.
    /// </remarks>
    public class CasoDeUsoObtenerListadoPacientes : IRequestHandler<ConsultaObtenerListadoPacientes,
                                                                List<PacienteListadoDTO>>
    {
        /// <summary>
        /// Repositorio utilizado para consultar los pacientes registrados.
        /// </summary>
        private readonly IRepositorioPacientes repositorio;

        /// <summary>
        /// Inicializa una nueva instancia del caso de uso de consulta
        /// de pacientes.
        /// </summary>
        /// <param name="repositorio">
        /// Repositorio encargado de proporcionar los pacientes almacenados.
        /// </param>
        public CasoDeUsoObtenerListadoPacientes(IRepositorioPacientes repositorio)
        {
            this.repositorio = repositorio;
        }

        /// <summary>
        /// Obtiene los pacientes registrados y transforma sus entidades
        /// en objetos de transferencia de datos.
        /// </summary>
        /// <param name="request">
        /// Consulta que solicita obtener el listado de pacientes.
        /// </param>
        /// <returns>
        /// Una lista de objetos PacienteListadoDTO con la información
        /// de los pacientes registrados.
        /// </returns>
        public async Task<List<PacienteListadoDTO>> Handle(ConsultaObtenerListadoPacientes request)
        {
            // Obtiene todas las entidades de pacientes almacenadas mediante el repositorio.
            var pacientes = await repositorio.ObtenerTodos();

            // Convierte cada entidad Paciente en su correspondiente DTO
            // y genera una lista con los resultados.
            var pacientesDTO = pacientes.Select(paciente => paciente.ADto()).ToList();

            // Retorna el listado de pacientes preparado para su consumo.
            return pacientesDTO;
        }
    }
}
