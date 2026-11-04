
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio
{
    /// <summary>
    /// Implementa el caso de uso encargado de eliminar un consultorio existente.
    /// </summary>
    public class CasoDeUsoBorrarConsultorio : IRequestHandler<ComandoBorrarConsultorio>
    {
        /// <summary>
        /// Repositorio encargado de consultar y gestionar las operaciones
        /// relacionadas con los consultorios.
        /// </summary>
        private readonly IRepositorioConsultorios repositorio;

        /// <summary>
        /// Unidad de trabajo encargada de confirmar o revertir
        /// los cambios realizados durante la operación de eliminación.
        /// </summary>
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        /// <summary>
        /// Inicializa el caso de uso con las dependencias necesarias
        /// para consultar y eliminar consultorios.
        /// </summary>
        /// <param name="repositorio">Repositorio encargado de gestionar los consultorios.</param>
        /// <param name="unidadDeTrabajo">Unidad de trabajo encargada de controlar la persistencia de los cambios.</param>
        public CasoDeUsoBorrarConsultorio(IRepositorioConsultorios repositorio, IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }

        /// <summary>
        /// Ejecuta la eliminación de un consultorio a partir de su identificador.
        /// Verifica que exista antes de solicitar su eliminación y persistir los cambios.
        /// </summary>
        /// <param name="request">Comando que contiene el identificador del consultorio que se desea eliminar.</param>
        /// <returns>Una tarea que representa la ejecución asíncrona de la eliminación.</returns>
        /// <exception cref="ExcepcionNoEncontrado">
        /// Se produce cuando no existe un consultorio con el identificador proporcionado.
        /// </exception>
        public async Task Handle(ComandoBorrarConsultorio request)
        {
            // Busca el consultorio que se desea eliminar mediante su identificador.
            var consultorio = await repositorio.ObtenerPorId(request.Id);

            // Comprueba que el consultorio exista antes de solicitar su eliminación.
            if (consultorio is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            try
            {
                // Solicita al repositorio eliminar el consultorio encontrado.
                await repositorio.Borrar(consultorio);

                // Confirma y persiste los cambios realizados durante la operación.
                await unidadDeTrabajo.Persistir();
            }
            catch (Exception)
            {
                // Solicita revertir los cambios cuando ocurre un error durante la operación.
                await unidadDeTrabajo.Reversar();

                // Propaga nuevamente la excepción para que pueda ser gestionada
                // por las capas superiores de la aplicación.
                throw;
            }
        }
    }
}
