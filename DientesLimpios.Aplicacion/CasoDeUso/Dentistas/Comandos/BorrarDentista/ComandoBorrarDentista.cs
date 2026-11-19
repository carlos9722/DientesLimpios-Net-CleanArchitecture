
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Comandos.BorrarDentista
{
    /// <summary>
    /// Representa la solicitud para eliminar un dentista existente.
    /// </summary>
    /// <remarks>
    /// Contiene el identificador único necesario para localizar
    /// el dentista que se desea eliminar.
    /// </remarks>
    public class ComandoBorrarDentista : IRequest
    {
        /// <summary>
        /// Obtiene o establece el identificador único del dentista que se desea eliminar.
        /// </summary>
        public Guid Id { get; set; }
    }
}
