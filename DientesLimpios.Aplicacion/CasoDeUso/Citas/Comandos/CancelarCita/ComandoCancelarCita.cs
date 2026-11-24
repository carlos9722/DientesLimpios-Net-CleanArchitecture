
using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasoDeUso.Citas.Comandos.CancelarCita
{
    /// <summary>
    /// Representa la solicitud para cancelar una cita.
    /// </summary>
    public class ComandoCancelarCita : IRequest
    {
        /// <summary>
        /// Identificador único de la cita que se desea cancelar.
        /// </summary>
        public required Guid Id { get; set; }
    }
}
