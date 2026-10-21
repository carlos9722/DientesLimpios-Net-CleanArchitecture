using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Comandos.CrearConsultorio
{
    /// <summary>
    /// Gestiona la creación de un consultorio y la persistencia de sus cambios.
    /// </summary>
    public class CasoDeUsoCrearConsultorio : IRequestHandler<ComandoCrearConsultorio, Guid>
    {
        private readonly IRepositorioConsultorios repositorio;
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        /// <summary>
        /// Inicializa el caso de uso con las dependencias necesarias.
        /// </summary>
        /// <param name="repositorio">Permite gestionar los consultorios.</param>
        /// <param name="unidadDeTrabajo">Controla la persistencia de los cambios.</param>
        public CasoDeUsoCrearConsultorio(
            IRepositorioConsultorios repositorio,
            IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }

        /// <summary>
        /// Crea un consultorio y devuelve su identificador.
        /// </summary>
        /// <param name="comando">Contiene los datos necesarios para crear el consultorio.</param>
        /// <returns>El identificador único del consultorio creado.</returns>
        /// <exception cref="Exception">
        /// Propaga cualquier excepción ocurrida durante la operación.
        /// </exception>
        public async Task<Guid> Handle(ComandoCrearConsultorio comando)
        {
            // Construye la entidad a partir de los datos del comando.
            var consultorio = new Consultorio(comando.Nombre);

            try
            {
                // Agrega el consultorio y persiste los cambios pendientes.
                var respuesta = await repositorio.Agregar(consultorio);
                await unidadDeTrabajo.Persistir();

                return respuesta.Id;
            }
            catch (Exception)
            {
                // Revierte los cambios ante un fallo y propaga la excepción.
                await unidadDeTrabajo.Reversar();
                throw;
            }
        }
    }
}
