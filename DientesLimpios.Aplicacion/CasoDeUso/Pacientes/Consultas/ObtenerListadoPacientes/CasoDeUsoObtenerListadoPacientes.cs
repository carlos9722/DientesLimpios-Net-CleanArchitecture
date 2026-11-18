using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Comunes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerListadoPacientes
{
    /// <summary>
    /// Gestiona el caso de uso para obtener el listado paginado de pacientes.
    /// </summary>
    /// <remarks>
    /// Consulta los pacientes mediante el repositorio, obtiene el total de registros
    /// y transforma las entidades en objetos PacienteListadoDTO.
    /// </remarks>
    public class CasoDeUsoObtenerListadoPacientes : IRequestHandler<ConsultaObtenerListadoPacientes,
                                                                PaginadoDTO<PacienteListadoDTO>>
    {
        /// <summary>
        /// Repositorio encargado de consultar la información de los pacientes.
        /// </summary>
        private readonly IRepositorioPacientes repositorio;

        /// <summary>
        /// Inicializa una nueva instancia del caso de uso.
        /// </summary>
        /// <param name="repositorio">
        /// Repositorio utilizado para acceder a la información de los pacientes.
        /// </param>
        public CasoDeUsoObtenerListadoPacientes(IRepositorioPacientes repositorio)
        {
            this.repositorio = repositorio;
        }

        /// <summary>
        /// Obtiene el listado paginado de pacientes y transforma sus entidades en DTO.
        /// </summary>
        /// <param name="request">
        /// Consulta que contiene los criterios de filtrado y paginación de los pacientes.
        /// </param>
        /// <returns>
        /// Un objeto PaginadoDTO con la lista de pacientes en formato PacienteListadoDTO
        /// y la cantidad total de registros disponibles.
        /// </returns>
        public async Task<PaginadoDTO<PacienteListadoDTO>> Handle(ConsultaObtenerListadoPacientes request)
        {
            // Obtiene los pacientes que cumplen con los criterios de consulta.
            var pacientes = await repositorio.ObtenerFiltrado(request);

            // Consulta la cantidad total de pacientes registrados.
            var totalPacientes = await repositorio.ObtenerCantitadTotalRegistros();

            // Transforma las entidades de pacientes en objetos DTO.
            var pacientesDTO = pacientes.Select(paciente => paciente.ADto()).ToList();

            var paginadoDTO = new PaginadoDTO<PacienteListadoDTO>
            {
                Elementos = pacientesDTO,
                Total = totalPacientes
            };

            // Retorna los resultados junto con la cantidad total de registros.
            return paginadoDTO;
        }
    }
}