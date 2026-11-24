
using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasoDeUso.Citas.Comandos.CompletarCita
{
    /// <summary>
    /// Representa la solicitud para completar una cita.
    /// </summary>
    public class ComandoCompletarCita : IRequest
    {
        /// <summary>
        /// Identificador único de la cita que se desea completar.
        /// </summary>
        public required Guid Id { get; set; }
    }
}
