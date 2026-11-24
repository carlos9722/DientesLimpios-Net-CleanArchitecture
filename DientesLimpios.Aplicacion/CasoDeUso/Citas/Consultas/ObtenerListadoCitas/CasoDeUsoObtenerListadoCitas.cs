
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Citas.Consultas.ObtenerListadoCitas
{
    /// <summary>
    /// Ejecuta la consulta para obtener el listado de citas
    /// según los criterios de búsqueda especificados.
    /// </summary>
    public class CasoDeUsoObtenerListadoCitas : IRequestHandler<ConsultaObtenerListadoCitas, List<CitaListadoDTO>>
    {
        /// <summary>
        /// Repositorio encargado de consultar la información de las citas.
        /// </summary>
        private readonly IRepositorioCitas repositorio;

        /// <summary>
        /// Inicializa el caso de uso con el repositorio de citas.
        /// </summary>
        /// <param name="repositorio">
        /// Repositorio utilizado para consultar las citas registradas.
        /// </param>
        public CasoDeUsoObtenerListadoCitas(IRepositorioCitas repositorio)
        {
            this.repositorio = repositorio;
        }


        /// <summary>
        /// Obtiene las citas que cumplen con los filtros proporcionados
        /// y convierte los resultados en objetos de transferencia de datos.
        /// </summary>
        /// <param name="request">
        /// Consulta que contiene los criterios de búsqueda de las citas.
        /// </param>
        /// <returns>
        /// Lista de citas con la información necesaria para su visualización.
        /// </returns>
        public async Task<List<CitaListadoDTO>> Handle(ConsultaObtenerListadoCitas request)
        {
            var citas = await repositorio.ObtenerFiltrado(request);
            var citasDTO = citas.Select(cita => cita.ADto()).ToList();
            return citasDTO;
        }
    }
}
