using DientesLimpios.Aplicacion.Excepciones;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    /// <summary>
    /// Implementa un mediador que valida las solicitudes y las envía a su manejador correspondiente.
    /// </summary>
    public class MediadorSimple : IMediator
    {
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Inicializa el mediador con el proveedor de servicios de la aplicación.
        /// </summary>
        /// <param name="serviceProvider">
        /// Permite resolver los validadores y manejadores registrados en la inyección de dependencias.
        /// </param>
        public MediadorSimple(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Valida una solicitud, si tiene un validador registrado, y la envía a su manejador.
        /// </summary>
        /// <typeparam name="TResponse">Tipo de respuesta esperada.</typeparam>
        /// <param name="request">Solicitud que se desea procesar.</param>
        /// <returns>La respuesta generada por el manejador.</returns>
        /// <exception cref="ExcepcionDeValidacion">
        /// Se lanza cuando la solicitud no cumple las reglas de validación.
        /// </exception>
        /// <exception cref="ExcepcionDeMediador">
        /// Se lanza cuando no existe un manejador registrado para la solicitud.
        /// </exception>
        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            // Busca un validador registrado para el tipo concreto de solicitud.
            var tipoValidador = typeof(IValidator<>).MakeGenericType(request.GetType());

            var validador = serviceProvider.GetService(tipoValidador);

            if (validador is not null)
            {
                // Invoca ValidateAsync mediante reflexión porque el tipo del validador se conoce en tiempo de ejecución.
                var metodoValidar = tipoValidador.GetMethod("ValidateAsync");
                var tareaValidar = (Task)metodoValidar!.Invoke(
                    validador,
                    new object[] { request, CancellationToken.None })!;

                await tareaValidar.ConfigureAwait(false);

                // Recupera el resultado de la validación una vez finalizada la tarea.
                var resultado = tareaValidar.GetType().GetProperty("Result");
                var validationResult = (ValidationResult)resultado!.GetValue(tareaValidar)!;

                if (!validationResult.IsValid)
                {
                    throw new ExcepcionDeValidacion(validationResult);
                }
            }

            // Construye el tipo de manejador correspondiente a la solicitud y su respuesta.
            var tipoCasoDeUso = typeof(IRequestHandler<,>)
                .MakeGenericType(request.GetType(), typeof(TResponse));

            var casoDeUso = serviceProvider.GetService(tipoCasoDeUso);

            if (casoDeUso is null)
            {
                throw new ExcepcionDeMediador(
                    $"No se encontró un handler para {request.GetType().Name}");
            }

            // Ejecuta el manejador y devuelve su respuesta.
            var metodo = tipoCasoDeUso.GetMethod("Handle")!;
            return await (Task<TResponse>)metodo.Invoke(
                casoDeUso,
                new object[] { request })!;
        }
    }
}

