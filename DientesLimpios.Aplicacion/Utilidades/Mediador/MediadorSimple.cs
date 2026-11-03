
using DientesLimpios.Aplicacion.Excepciones;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion.Utilidades.Mediador
{
    /// <summary>
    /// Implementa el mediador encargado de validar las solicitudes y enviarlas
    /// al manejador correspondiente para ejecutar el caso de uso solicitado.
    /// </summary>
    public class MediadorSimple : IMediator
    {
        /// <summary>
        /// Proveedor de servicios utilizado para obtener las instancias
        /// de los validadores y manejadores registrados en la aplicación.
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// Inicializa el mediador con el proveedor de servicios de la aplicación.
        /// </summary>
        /// <param name="serviceProvider">
        /// Permite resolver los validadores y manejadores registrados
        /// mediante el contenedor de inyección de dependencias.
        /// </param>
        public MediadorSimple(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Valida una solicitud, si tiene un validador registrado, y posteriormente
        /// la envía al manejador correspondiente para obtener una respuesta.
        /// </summary>
        /// <typeparam name="TResponse">Tipo de dato que tendrá la respuesta generada.</typeparam>
        /// <param name="request">Solicitud que se desea procesar.</param>
        /// <returns>Una tarea que contiene la respuesta generada por el manejador.</returns>
        /// <exception cref="ExcepcionDeValidacion">
        /// Se produce cuando la solicitud no cumple las reglas de validación establecidas.
        /// </exception>
        /// <exception cref="ExcepcionDeMediador">
        /// Se produce cuando no existe un manejador registrado para procesar la solicitud.
        /// </exception>
        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            // Ejecuta las validaciones de la solicitud antes de procesarla.
            await RealizarValidaciones(request);

            // Construye dinámicamente el tipo de manejador correspondiente a la solicitud
            // y al tipo de respuesta esperado.
            var tipoCasoDeUso = typeof(IRequestHandler<,>)
        .MakeGenericType(request.GetType(), typeof(TResponse));

            // Obtiene del contenedor de dependencias el manejador registrado.
            var casoDeUso = serviceProvider.GetService(tipoCasoDeUso);

            // Verifica que exista un manejador para procesar la solicitud.
            if (casoDeUso is null)
            {
                throw new ExcepcionDeMediador($"No se encontró un handler para {request.GetType().Name}");
            }

            // Obtiene e invoca el método Handle del manejador utilizando reflexión.
            var metodo = tipoCasoDeUso.GetMethod("Handle")!;

            // Espera la ejecución del manejador y devuelve la respuesta generada.
            return await (Task<TResponse>)metodo.Invoke(casoDeUso, new object[] { request })!;
        }

        /// <summary>
        /// Valida una solicitud que no espera una respuesta y posteriormente
        /// la envía al manejador correspondiente para ejecutar la operación.
        /// </summary>
        /// <param name="request">Solicitud que se desea procesar.</param>
        /// <returns>Una tarea que representa la ejecución asíncrona de la operación.</returns>
        /// <exception cref="ExcepcionDeValidacion">
        /// Se produce cuando la solicitud no cumple las reglas de validación establecidas.
        /// </exception>
        /// <exception cref="ExcepcionDeMediador">
        /// Se produce cuando no existe un manejador registrado para procesar la solicitud.
        /// </exception>
        public async Task Send(IRequest request)
        {
            // Ejecuta las validaciones de la solicitud antes de procesarla.
            await RealizarValidaciones(request);

            // Construye dinámicamente el tipo de manejador correspondiente a la solicitud.
            var tipoCasoDeUso = typeof(IRequestHandler<>).MakeGenericType(request.GetType());

            // Obtiene del contenedor de dependencias el manejador registrado.
            var casoDeUso = serviceProvider.GetService(tipoCasoDeUso);

            // Verifica que exista un manejador para procesar la solicitud.
            if (casoDeUso is null)
            {
                throw new ExcepcionDeMediador($"No se encontró un handler para {request.GetType().Name}");
            }

            // Obtiene e invoca el método Handle del manejador utilizando reflexión.
            var metodo = tipoCasoDeUso.GetMethod("Handle")!;

            // Espera a que finalice la ejecución del manejador sin obtener una respuesta.
            await (Task)metodo.Invoke(casoDeUso, new object[] { request })!;
        }

        /// <summary>
        /// Busca y ejecuta el validador asociado al tipo de solicitud.
        /// Si no existe un validador registrado, permite continuar con el procesamiento.
        /// </summary>
        /// <param name="request">Objeto de solicitud que se desea validar.</param>
        /// <returns>Una tarea que representa la ejecución asíncrona de las validaciones.</returns>
        /// <exception cref="ExcepcionDeValidacion">
        /// Se produce cuando el resultado de la validación contiene errores.
        /// </exception>
        private async Task RealizarValidaciones(object request)
        {
            // Construye dinámicamente el tipo de validador correspondiente a la solicitud.
            var tipoValidador = typeof(IValidator<>).MakeGenericType(request.GetType());

            // Busca el validador registrado en el contenedor de dependencias.
            var validador = serviceProvider.GetService(tipoValidador);

            // Ejecuta la validación únicamente si existe un validador registrado.
            if (validador is not null)
            {
                // Obtiene el método ValidateAsync del validador mediante reflexión.
                var metodoValidar = tipoValidador.GetMethod("ValidateAsync");

                // Invoca la validación asíncrona utilizando la solicitud y un token
                // que indica que no se solicita la cancelación de la operación.
                var tareaValidar = (Task)metodoValidar!.Invoke(validador,
                        new object[] { request, CancellationToken.None })!;

                // Espera a que finalice el proceso de validación.
                await tareaValidar.ConfigureAwait(false);

                // Recupera el resultado de la validación una vez finalizada la tarea.
                var resultado = tareaValidar.GetType().GetProperty("Result");
                var validationResult = (ValidationResult)resultado!.GetValue(tareaValidar)!;

                // Verifica si existen errores y lanza una excepción cuando la solicitud
                // no cumple alguna de las reglas de validación.
                if (!validationResult.IsValid)
                {
                    throw new ExcepcionDeValidacion(validationResult);
                }
            }
        }
    }

}
