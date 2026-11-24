
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;

namespace DientesLimpios.Aplicacion.CasoDeUso.Citas.Comandos.CompletarCita
{
    /// <summary>
    /// Ejecuta el caso de uso encargado de completar una cita.
    /// </summary>
    public class CasoDeUsoCompletarCita : IRequestHandler<ComandoCompletarCita>
    {
        /// <summary>
        /// Repositorio encargado de consultar y actualizar las citas.
        /// </summary>
        private readonly IRepositorioCitas repositorio;

        /// <summary>
        /// Gestiona la persistencia y reversión de los cambios en la base de datos.
        /// </summary>
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        /// <summary>
        /// Inicializa el caso de uso con las dependencias necesarias.
        /// </summary>
        /// <param name="repositorio">Repositorio para acceder a las citas.</param>
        /// <param name="unidadDeTrabajo">Servicio encargado de gestionar la persistencia de los cambios.</param>
        public CasoDeUsoCompletarCita(
            IRepositorioCitas repositorio,
            IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }

        /// <summary>
        /// Busca la cita, cambia su estado a completada y persiste los cambios.
        /// Si ocurre un error durante la persistencia, revierte la operación.
        /// </summary>
        /// <param name="request">Comando que contiene el identificador de la cita.</param>
        public async Task Handle(ComandoCompletarCita request)
        {
            var cita = await repositorio.ObtenerPorId(request.Id);

            // Verifica que la cita exista antes de modificarla.
            if (cita is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            // Ejecuta la regla de negocio para completar la cita.
            cita.Completar();

            try
            {
                await repositorio.Actualizar(cita);
                await unidadDeTrabajo.Persistir();
            }
            catch (Exception)
            {
                // Revierte los cambios si ocurre un error durante la persistencia.
                await unidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
