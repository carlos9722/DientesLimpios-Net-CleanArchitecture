
using DientesLimpios.Aplicacion.Contratos.Notificaciones;
using DientesLimpios.Dominio.Entidades;

namespace DientesLimpios.Aplicacion.CasoDeUso.Citas.Comandos.EnviarRecordatorioCitas
{
    /// <summary>
    /// Proporciona métodos de extensión para convertir entidades del dominio en DTOs de recordatorios.
    /// </summary>
    public static class MapeadorExtensions
    {
        /// <summary>
        /// Convierte una entidad Cita en un DTO con la información necesaria
        /// para enviar un recordatorio al paciente.
        /// </summary>
        /// <param name="cita">Entidad de dominio que contiene la información de la cita.</param>
        /// <returns>DTO con los datos necesarios para enviar el recordatorio.</returns>
        public static RecordatorioCitaDTO ADto(this Cita cita)
        {
            return new RecordatorioCitaDTO
            {
                Id = cita.Id,
                Fecha = cita.IntervaloDeTiempo.Inicio,
                Paciente = cita.Paciente!.Nombre,
                Paciente_Email = cita.Paciente.Email.Valor,
                Consultorio = cita.Consultorio!.Nombre,
                Dentista = cita.Dentista!.Nombre
            };
        }
    }
}
