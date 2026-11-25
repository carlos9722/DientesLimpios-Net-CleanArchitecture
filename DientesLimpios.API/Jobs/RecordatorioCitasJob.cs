
using DientesLimpios.Aplicacion.CasoDeUso.Citas.Comandos.EnviarRecordatorioCitas;
using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.API.Jobs
{
    /// <summary>
    /// Servicio en segundo plano encargado de programar el envío de recordatorios de citas.
    /// </summary>
    public class RecordatorioCitasJob : BackgroundService
    {
        /// <summary>
        /// Permite crear ámbitos de dependencias para resolver servicios registrados como Scoped.
        /// </summary>
        private readonly IServiceScopeFactory scopeFactory;

        /// <summary>
        /// Zona horaria utilizada para determinar el horario de envío de los recordatorios.
        /// </summary>
        private readonly TimeZoneInfo zonaHorariaRD =
            TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        /// <summary>
        /// Inicializa el servicio con la fábrica de ámbitos de dependencias.
        /// </summary>
        /// <param name="scopeFactory">Fábrica utilizada para crear ámbitos de servicios.</param>
        public RecordatorioCitasJob(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        /// <summary>
        /// Ejecuta el proceso de envío de recordatorios mientras la aplicación permanezca activa.
        /// </summary>
        /// <param name="stoppingToken">Token que permite cancelar la ejecución del servicio.</param>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var ahora = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaHorariaRD);

                // Ejecuta el envío de recordatorios cuando son las 8:00 a. m.
                if (ahora.Hour == 8)
                {
                    using var scope = scopeFactory.CreateScope();

                    var mediador = scope.ServiceProvider.GetRequiredService<IMediator>();

                    await mediador.Send(new ComandoEnviarRecordatorioCitas());
                }

                // Espera una hora antes de volver a comprobar el horario.
                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }
    }
}
