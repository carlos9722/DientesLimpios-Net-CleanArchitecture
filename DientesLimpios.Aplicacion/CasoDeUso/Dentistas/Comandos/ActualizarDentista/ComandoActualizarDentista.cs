
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Dentistas.Comandos.ActualizarDentista
{
    /// <summary>
    /// Representa la solicitud para actualizar la información de un dentista.
    /// </summary>
    /// <remarks>
    /// Contiene los datos necesarios para identificar al dentista
    /// y proporcionar la nueva información que se desea actualizar.
    /// </remarks>
    public class ComandoActualizarDentista : IRequest
    {
        /// <summary>
        /// Obtiene o establece el identificador único del dentista que se desea actualizar.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Obtiene o establece el nuevo nombre del dentista.
        /// </summary>
        public required string Nombre { get; set; }

        /// <summary>
        /// Obtiene o establece el nuevo correo electrónico del dentista.
        /// </summary>
        public required string Email { get; set; }
    }
}
