
using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Excepciones;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio
{
    /// <summary>
    /// Implementa el caso de uso encargado de actualizar la información
    /// de un consultorio existente.
    /// </summary>
    public class CasoDeUsoActualizarConsultorio : IRequestHandler<ComandoActualizarConsultorio>
    {
        /// <summary>
        /// Repositorio utilizado para consultar y actualizar los consultorios.
        /// </summary>
        private readonly IRepositorioConsultorios repositorio;

        /// <summary>
        /// Unidad de trabajo encargada de confirmar o revertir los cambios
        /// realizados durante la operación de persistencia.
        /// </summary>
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        /// <summary>
        /// Inicializa el caso de uso con las dependencias necesarias
        /// para consultar y persistir los cambios del consultorio.
        /// </summary>
        /// <param name="repositorio">Repositorio encargado de gestionar los consultorios.</param>
        /// <param name="unidadDeTrabajo">Unidad de trabajo encargada de controlar la persistencia de los cambios.</param>
        public CasoDeUsoActualizarConsultorio(IRepositorioConsultorios repositorio,
            IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }

        /// <summary>
        /// Ejecuta la actualización de un consultorio existente.
        /// Busca el consultorio, valida la nueva información y persiste los cambios.
        /// </summary>
        /// <param name="request">Comando que contiene el identificador y el nuevo nombre del consultorio.</param>
        /// <returns>Una tarea que representa la ejecución asíncrona de la actualización.</returns>
        /// <exception cref="ExcepcionNoEncontrado">
        /// Se produce cuando no existe un consultorio con el identificador proporcionado.
        /// </exception>
        public async Task Handle(ComandoActualizarConsultorio request)
        {
            // Busca el consultorio que se desea actualizar mediante su identificador.
            var consultorio = await repositorio.ObtenerPorId(request.Id);

            // Verifica que el consultorio exista antes de realizar cualquier modificación.
            if (consultorio is null)
            {
                throw new ExcepcionNoEncontrado();
            }

            // Actualiza el nombre utilizando el método de la entidad,
            // que se encarga de aplicar las reglas de negocio correspondientes.
            consultorio.ActualizarNombre(request.Nombre);

            try
            {
                // Solicita al repositorio actualizar la información del consultorio.
                await repositorio.Actualizar(consultorio);

                // Confirma y persiste los cambios realizados en la operación.
                await unidadDeTrabajo.Persistir();
            }
            catch (Exception)
            {
                // Revierte los cambios cuando ocurre un error durante la persistencia.
                await unidadDeTrabajo.Reversar();

                // Propaga nuevamente la excepción para que pueda ser gestionada
                // por las capas superiores de la aplicación.
                throw;
            }

        }
    }
}
