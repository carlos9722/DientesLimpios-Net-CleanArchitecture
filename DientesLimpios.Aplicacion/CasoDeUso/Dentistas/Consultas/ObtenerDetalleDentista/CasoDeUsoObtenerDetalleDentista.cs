
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Consultas.ObtenerDetalleDentista
{
    /// <summary>
    /// Gestiona la consulta del detalle de un dentista mediante su identificador.
    /// </summary>
    /// <remarks>
    /// Coordina la recuperación de la información del dentista mediante el repositorio
    /// y transforma la entidad del dominio en un objeto de transferencia de datos (DTO).
    /// </remarks>
    public class CasoDeUsoObtenerDetalleDentista : IRequestHandler<ConsultaObtenerDetalleDentista, DentistaDTO>
    {
        // Dependencia encargada de consultar la información de los dentistas.
        private readonly IRepositorioDentista repositorio;

        /// <summary>
        /// Inicializa una instancia del caso de uso para consultar el detalle de un dentista.
        /// </summary>
        /// <param name="repositorio">
        /// Repositorio encargado de recuperar la información del dentista.
        /// </param>
        public CasoDeUsoObtenerDetalleDentista(IRepositorioDentista repositorio)
        {
            this.repositorio = repositorio;
        }

        /// <summary>
        /// Ejecuta la consulta del detalle de un dentista.
        /// </summary>
        /// <param name="request">
        /// Consulta que contiene el identificador del dentista que se desea recuperar.
        /// </param>
        /// <returns>
        /// Una tarea que representa la ejecución asíncrona y devuelve un DTO
        /// con la información del dentista consultado.
        /// </returns>
        /// <exception cref="ExcepcionNoEncontrado">
        /// Se lanza cuando no existe un dentista con el identificador proporcionado.
        /// </exception>
        /// <remarks>
        /// Recupera el dentista mediante su identificador, verifica su existencia
        /// y transforma la entidad en un DTO para devolver la información solicitada.
        /// </remarks>
        public async Task<DentistaDTO> Handle(ConsultaObtenerDetalleDentista request)
        {
            // Busca el dentista mediante el identificador recibido en la consulta.
            var dentista = await repositorio.ObtenerPorId(request.Id);

            // Verifica que el dentista exista antes de devolver su información.
            if (dentista is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            // Transforma la entidad del dominio en un DTO para entregar los datos consultados.
            return dentista.ADto();
        }
    }
}
