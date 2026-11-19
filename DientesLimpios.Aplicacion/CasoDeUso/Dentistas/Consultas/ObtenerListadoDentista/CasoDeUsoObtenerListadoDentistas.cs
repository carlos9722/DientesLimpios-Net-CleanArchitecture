
using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentistas;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Comunes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Consultas.ObtenerListadoDentista
{
    /// <summary>
    /// Gestiona la consulta del listado de dentistas mediante criterios de filtrado y paginación.
    /// </summary>
    /// <remarks>
    /// Coordina la recuperación de los dentistas mediante el repositorio,
    /// transforma las entidades del dominio en DTO y organiza los resultados
    /// junto con la cantidad total de registros.
    /// </remarks>
    public class CasoDeUsoObtenerListadoDentistas : IRequestHandler<ConsultaObtenerListadoDentistas, PaginadoDTO<DentistaListadoDTO>>
    {
        // Dependencia encargada de consultar la información de los dentistas.
        private readonly IRepositorioDentista repositorio;

        /// <summary>
        /// Inicializa una instancia del caso de uso para consultar el listado de dentistas.
        /// </summary>
        /// <param name="repositorio">
        /// Repositorio encargado de recuperar y contar los registros de dentistas.
        /// </param>
        public CasoDeUsoObtenerListadoDentistas(IRepositorioDentista repositorio)
        {
            this.repositorio = repositorio;
        }

        /// <summary>
        /// Ejecuta la consulta del listado de dentistas aplicando los criterios recibidos.
        /// </summary>
        /// <param name="request">
        /// Consulta que contiene los criterios de filtrado y paginación del listado.
        /// </param>
        /// <returns>
        /// Una tarea que representa la ejecución asíncrona y devuelve un objeto
        /// PaginadoDTO con los dentistas consultados y la cantidad total de registros.
        /// </returns>
        /// <remarks>
        /// Recupera los registros filtrados, obtiene la cantidad total de dentistas,
        /// transforma las entidades en DTO y construye la respuesta paginada.
        /// </remarks>
        public async Task<PaginadoDTO<DentistaListadoDTO>> Handle(ConsultaObtenerListadoDentistas request)
        {
            // Recupera los dentistas aplicando los filtros y parámetros de paginación recibidos.
            var Dentistas = await repositorio.ObtenerFiltrado(request);

            // Obtiene la cantidad total de registros para informar el total de resultados disponibles.
            var totalDentistas = await repositorio.ObtenerCantitadTotalRegistros();

            // Transforma cada entidad Dentista en su correspondiente DTO de listado.
            var DentistasDTO = Dentistas.Select(dentista => dentista.ADto()).ToList();

            // Construye el objeto de respuesta con los elementos obtenidos y el total de registros.
            var paginadoDTO = new PaginadoDTO<DentistaListadoDTO>
            {
                Elementos = DentistasDTO,
                Total = totalDentistas
            };

            // Devuelve el resultado del listado con su información de paginación.
            return paginadoDTO;
        }
    }
}
