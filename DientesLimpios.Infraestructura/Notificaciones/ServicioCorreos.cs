
using DientesLimpios.Aplicacion.Contratos.Notificaciones;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Net;
using System.Net.Mail;

namespace DientesLimpios.Infraestructura.Notificaciones
{
    /// <summary>
    /// Implementa el servicio de notificaciones mediante el envío de correos electrónicos.
    /// </summary>
    public class ServicioCorreos : IServicioNotificaciones
    {
        /// <summary>
        /// Proporciona acceso a la configuración del servidor de correo.
        /// </summary>
        private readonly IConfiguration configuration;

        /// <summary>
        /// Inicializa el servicio con la configuración de la aplicación.
        /// </summary>
        /// <param name="configuration">Configuración utilizada para obtener los datos del servidor SMTP.</param>
        public ServicioCorreos(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Envía un correo electrónico con los detalles de la cita programada.
        /// </summary>
        /// <param name="cita">Información del paciente y de la cita que se desea confirmar.</param>
        public async Task EnviarConfirmacionCita(ConfirmacionCitaDTO cita)
        {
            var asunto = "Confirmación de cita - Dientes Limpios";

            var cuerpo = $"""
                Estimado (a) {cita.Paciente},

                Su cita con el Dr (Dra.) {cita.Dentista} ha sido programada para el {cita.Fecha.ToString("f", new CultureInfo("es-DO"))} en el consultorio {cita.Consultorio}.

                ¡Le esperamos!

                Equipo de Dientes Limpios
                """;

            await EnviarMensaje(cita.Paciente_Email, asunto, cuerpo);
        }

        /// <summary>
        /// Envía un correo electrónico para recordar al paciente los detalles de su cita.
        /// </summary>
        /// <param name="cita">Información del paciente y de la cita que se desea recordar.</param>
        public async Task EnviarRecordatorioCita(RecordatorioCitaDTO cita)
        {
            var asunto = "RECORDATORIO: Confirmación de cita - Dientes Limpios";

            var cuerpo = $"""
                Estimado (a) {cita.Paciente},

                Le recordamos que tiene cita con el Dr (Dra.) {cita.Dentista} para el {cita.Fecha.ToString("f", new CultureInfo("es-DO"))} en el consultorio {cita.Consultorio}.

                ¡Le esperamos!

                Equipo de Dientes Limpios
                """;

            await EnviarMensaje(cita.Paciente_Email, asunto, cuerpo);
        }

        /// <summary>
        /// Configura el servidor SMTP y envía el correo electrónico al destinatario.
        /// </summary>
        /// <param name="emailDestinatario">Dirección de correo electrónico del destinatario.</param>
        /// <param name="asunto">Asunto del correo electrónico.</param>
        /// <param name="cuerpo">Contenido del mensaje que se enviará.</param>
        private async Task EnviarMensaje(string emailDestinatario, string asunto, string cuerpo)
        {
            var nuestroEmail = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:EMAIL");
            var password = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:PASSWORD");
            var host = configuration.GetValue<string>("CONFIGURACIONES_EMAIL:HOST");
            var puerto = configuration.GetValue<int>("CONFIGURACIONES_EMAIL:PUERTO");

            var smtpCliente = new SmtpClient(host, puerto);
            smtpCliente.EnableSsl = true;
            smtpCliente.UseDefaultCredentials = false;
            smtpCliente.Credentials = new NetworkCredential(nuestroEmail, password);

            var mensaje = new MailMessage(nuestroEmail!, emailDestinatario, asunto, cuerpo);

            await smtpCliente.SendMailAsync(mensaje);
        }
    }
}
