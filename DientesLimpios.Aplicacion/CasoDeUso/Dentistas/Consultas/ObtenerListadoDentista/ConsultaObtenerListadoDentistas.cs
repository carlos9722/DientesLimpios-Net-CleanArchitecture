
using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentistas;
using DientesLimpios.Aplicacion.Utilidades.Comunes;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Consultas.ObtenerListadoDentista
{
    /// <summary>
    /// Representa la solicitud para obtener el listado de dentistas aplicando filtros y paginación.
    /// </summary>
    /// <remarks>
    /// Hereda los criterios de búsqueda y paginación de FiltroDentistasDTO
    /// y define el tipo de respuesta que devolverá el manejador mediante
    /// un objeto PaginadoDTO con los registros de tipo DentistaListadoDTO.
    /// </remarks>
    public class ConsultaObtenerListadoDentistas : FiltroDentistasDTO, IRequest<PaginadoDTO<DentistaListadoDTO>>
    {
    }
}
