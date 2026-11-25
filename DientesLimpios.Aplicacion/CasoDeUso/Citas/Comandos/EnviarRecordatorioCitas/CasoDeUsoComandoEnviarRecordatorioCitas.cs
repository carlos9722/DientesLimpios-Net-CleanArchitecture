
using DientesLimpios.Aplicacion.Contratos.Notificaciones;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Contratos.Repositorios.Modelos;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Enums;

namespace DientesLimpios.Aplicacion.CasoDeUso.Citas.Comandos.EnviarRecordatorioCitas
{
    /// <summary>
    /// Ejecuta el proceso de búsqueda de citas programadas para el día siguiente
    /// y envía los recordatorios correspondientes a los pacientes.
    /// </summary>
    public class CasoDeUsoComandoEnviarRecordatorioCitas : IRequestHandler<ComandoEnviarRecordatorioCitas>
    {
        /// <summary>
        /// Repositorio encargado de consultar las citas programadas.
        /// </summary>
        private readonly IRepositorioCitas repositorio;

        /// <summary>
        /// Servicio encargado de enviar las notificaciones a los pacientes.
        /// </summary>
        private readonly IServicioNotificaciones servicioNotificaciones;

        /// <summary>
        /// Inicializa el caso de uso con las dependencias necesarias.
        /// </summary>
        /// <param name="repositorio">Repositorio utilizado para consultar las citas.</param>
        /// <param name="servicioNotificaciones">Servicio encargado del envío de notificaciones.</param>
        public CasoDeUsoComandoEnviarRecordatorioCitas(
            IRepositorioCitas repositorio,
            IServicioNotificaciones servicioNotificaciones)
        {
            this.repositorio = repositorio;
            this.servicioNotificaciones = servicioNotificaciones;
        }

        /// <summary>
        /// Consulta las citas programadas para el día siguiente y envía un recordatorio a cada paciente.
        /// </summary>
        /// <param name="request">Comando que solicita ejecutar el proceso de recordatorios.</param>
        public async Task Handle(ComandoEnviarRecordatorioCitas request)
        {
            var mañana = DateTime.UtcNow.Date.AddDays(1);
            var fechaInicio = mañana;
            var fechaFin = mañana.AddDays(1);

            var filtro = new FiltroCitasDTO
            {
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                EstadoCita = EstadoCita.Programada
            };

            var citas = await repositorio.ObtenerFiltrado(filtro);

            foreach (var cita in citas)
            {
                var citaDTO = cita.ADto();
                await servicioNotificaciones.EnviarRecordatorioCita(citaDTO);
            }
        }
    }
}
