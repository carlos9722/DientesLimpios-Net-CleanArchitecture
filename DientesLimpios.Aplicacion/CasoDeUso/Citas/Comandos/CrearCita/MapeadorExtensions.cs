
using DientesLimpios.Aplicacion.Contratos.Notificaciones;
using DientesLimpios.Dominio.Entidades;

namespace DientesLimpios.Aplicacion.CasoDeUso.Citas.Comandos.CrearCita
{
    /// <summary>
    /// Proporciona métodos de extensión para convertir entidades del dominio en DTOs.
    /// </summary>
    public static class MapeadorExtensions
    {
        /// <summary>
        /// Convierte una entidad Cita en un DTO con la información necesaria
        /// para enviar la confirmación de la cita.
        /// </summary>
        /// <param name="cita">Entidad de dominio que contiene la información de la cita.</param>
        /// <returns>DTO con los datos necesarios para enviar la notificación.</returns>
        public static ConfirmacionCitaDTO ADto(this Cita cita)
        {
            return new ConfirmacionCitaDTO
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
