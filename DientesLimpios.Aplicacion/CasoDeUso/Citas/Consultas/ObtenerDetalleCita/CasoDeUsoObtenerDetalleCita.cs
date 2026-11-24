
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Citas.Consultas.ObtenerDetalleCita
{
    /// <summary>
    /// Ejecuta la consulta para obtener el detalle de una cita
    /// mediante su identificador.
    /// </summary>
    public class CasoDeUsoObtenerDetalleCita : IRequestHandler<ConsultaObtenerDetalleCita, CitaDetalleDTO>
    {
        /// <summary>
        /// Repositorio encargado de consultar la información de las citas.
        /// </summary>
        private readonly IRepositorioCitas repositorio;

        /// <summary>
        /// Inicializa el caso de uso con el repositorio de citas.
        /// </summary>
        /// <param name="repositorio">
        /// Repositorio utilizado para obtener la información de una cita.
        /// </param>
        public CasoDeUsoObtenerDetalleCita(IRepositorioCitas repositorio)
        {
            this.repositorio = repositorio;
        }

        /// <summary>
        /// Obtiene la información de una cita y la convierte en un DTO
        /// para devolver sus datos de detalle.
        /// </summary>
        /// <param name="request">
        /// Consulta que contiene el identificador de la cita que se desea obtener.
        /// </param>
        /// <returns>
        /// DTO con la información detallada de la cita encontrada.
        /// </returns>
        /// <remarks>
        /// Si la cita no existe, se genera una excepción de tipo ExcepcionNoEncontrado.
        /// </remarks>
        public async Task<CitaDetalleDTO> Handle(ConsultaObtenerDetalleCita request)
        {
            var cita = await repositorio.ObtenerPorId(request.Id);

            if (cita is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            return cita.ADto();
        }
    }
}
