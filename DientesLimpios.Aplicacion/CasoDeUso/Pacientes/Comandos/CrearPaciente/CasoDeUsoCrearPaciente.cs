using DientesLimpios.Aplicacion.Contratos.Persistencia;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Pacientes.Comandos.CrearPaciente
{
    /// <summary>
    /// Implementa el caso de uso encargado de crear un nuevo paciente
    /// dentro de la aplicación.
    /// </summary>
    /// <remarks>
    /// Procesa la solicitud de creación, construye los objetos del dominio
    /// y coordina su persistencia mediante el repositorio y la unidad de trabajo.
    /// </remarks>
    public class CasoDeUsoCrearPaciente : IRequestHandler<ComandoCrearPaciente, Guid>
    {
        /// <summary>
        /// Repositorio utilizado para realizar las operaciones de persistencia
        /// relacionadas con los pacientes.
        /// </summary>
        private readonly IRepositorioPacientes repositorio;

        /// <summary>
        /// Unidad de trabajo encargada de confirmar o revertir
        /// las operaciones realizadas durante el proceso de persistencia.
        /// </summary>
        private readonly IUnidadDeTrabajo unidadDeTrabajo;

        /// <summary>
        /// Inicializa una nueva instancia del caso de uso de creación de pacientes.
        /// </summary>
        /// <param name="repositorio">
        /// Repositorio utilizado para agregar el nuevo paciente.
        /// </param>
        /// <param name="unidadDeTrabajo">
        /// Unidad de trabajo utilizada para confirmar o revertir
        /// los cambios realizados en la base de datos.
        /// </param>
        public CasoDeUsoCrearPaciente(
            IRepositorioPacientes repositorio,
            IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.repositorio = repositorio;
            this.unidadDeTrabajo = unidadDeTrabajo;
        }

        /// <summary>
        /// Procesa la solicitud de creación de un paciente y persiste
        /// la información en la base de datos.
        /// </summary>
        /// <param name="request">
        /// Comando que contiene los datos necesarios para crear el paciente.
        /// </param>
        /// <returns>
        /// Un identificador único que representa al paciente creado.
        /// </returns>
        /// <exception cref="Exception">
        /// Se propaga cualquier excepción producida durante la persistencia,
        /// después de intentar revertir los cambios realizados.
        /// </exception>
        public async Task<Guid> Handle(ComandoCrearPaciente request)
        {
            // Construye el objeto de valor Email, aplicando sus validaciones de dominio.
            var email = new Email(request.Email);

            // Crea la entidad Paciente aplicando las reglas de negocio correspondientes.
            var paciente = new Paciente(request.Nombre, email);

            try
            {
                // Agrega el paciente mediante el repositorio y obtiene la entidad resultante.
                var respuesta = await repositorio.Agregar(paciente);

                // Confirma los cambios pendientes en la base de datos.
                await unidadDeTrabajo.Persistir();

                // Retorna el identificador del paciente creado.
                return respuesta.Id;
            }
            catch (Exception)
            {
                // Intenta revertir los cambios cuando ocurre un error durante la persistencia.
                await unidadDeTrabajo.Reversar();

                // Propaga la excepción para que pueda ser gestionada por una capa superior.
                throw;
            }
        }
    }
}
