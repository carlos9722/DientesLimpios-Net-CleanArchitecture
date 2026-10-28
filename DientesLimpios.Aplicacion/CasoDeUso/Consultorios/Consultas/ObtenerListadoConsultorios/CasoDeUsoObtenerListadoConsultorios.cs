
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Consultas.ObtenerListadoConsultorios
{
    /// <summary>
    /// Gestiona la consulta y transformación del listado de consultorios.
    /// </summary>
    public class CasoDeUsoObtenerListadoConsultorios : IRequestHandler<ConsultaObtenerListadoConsultorios,
                                                            List<ConsultorioListadoDTO>>
    {
        /// <summary>
        /// Repositorio encargado de acceder a los datos de los consultorios.
        /// </summary>
        private readonly IRepositorioConsultorios repositorio;

        /// <summary>
        /// Inicializa el caso de uso con el repositorio de consultorios.
        /// </summary>
        /// <param name="repositorio">Repositorio para consultar los consultorios.</param>
        public CasoDeUsoObtenerListadoConsultorios(IRepositorioConsultorios repositorio)
        {
            this.repositorio = repositorio;
        }

        /// <summary>
        /// Obtiene los consultorios y convierte sus entidades en DTOs.
        /// </summary>
        /// <param name="request">Solicitud para obtener el listado de consultorios.</param>
        /// <returns>Lista de consultorios representados mediante DTOs.</returns>
        public async Task<List<ConsultorioListadoDTO>> Handle(ConsultaObtenerListadoConsultorios request)
        {
            var consultorios = await repositorio.ObtenerTodos();
            var consultoriosDTO = consultorios.Select(consultorio => consultorio.ADto()).ToList();
            return consultoriosDTO;
        }
    }
}
