
namespace DientesLimpios.Aplicacion.Contratos.Notificaciones
{
    /// <summary>
    /// Define las operaciones disponibles para el envío de notificaciones relacionadas con las citas.
    /// </summary>
    public interface IServicioNotificaciones
    {
        /// <summary>
        /// Envía un correo electrónico para confirmar la programación de una cita.
        /// </summary>
        /// <param name="cita">Información del paciente y de la cita que se desea confirmar.</param>
        Task EnviarConfirmacionCita(ConfirmacionCitaDTO cita);

        /// <summary>
        /// Envía un correo electrónico para recordar al paciente su próxima cita.
        /// </summary>
        /// <param name="cita">Información del paciente y de la cita que se desea recordar.</param>
        Task EnviarRecordatorioCita(RecordatorioCitaDTO cita);
    }
}
