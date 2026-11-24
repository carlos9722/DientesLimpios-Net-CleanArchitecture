
using DientesLimpios.Aplicacion.Contratos.Repositorios.Modelos;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Citas.Consultas.ObtenerListadoCitas
{
    /// <summary>
    /// Representa la solicitud para obtener el listado de citas
    /// aplicando los criterios de filtrado disponibles.
    /// </summary>
    public class ConsultaObtenerListadoCitas : FiltroCitasDTO, IRequest<List<CitaListadoDTO>>
    {
    }
}
