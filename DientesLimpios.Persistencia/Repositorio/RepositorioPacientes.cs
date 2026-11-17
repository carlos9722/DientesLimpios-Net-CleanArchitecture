using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Persistencia.Repositorio
{
    /// <summary>
    /// Implementa el repositorio encargado de gestionar las operaciones
    /// de persistencia relacionadas con la entidad Paciente.
    /// </summary>
    /// <remarks>
    /// Hereda las operaciones genéricas de Repositorio<Paciente>
    /// e implementa el contrato IRepositorioPacientes definido
    /// en la capa de aplicación.
    /// </remarks>
    public class RepositorioPacientes : Repositorio<Paciente>, IRepositorioPacientes
    {
        /// <summary>
        /// Representa el contexto de Entity Framework Core utilizado
        /// para acceder a la base de datos.
        /// </summary>
        private readonly DientesLimpiosDbContext context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio de pacientes.
        /// </summary>
        /// <param name="context">
        /// Contexto de base de datos utilizado para realizar las operaciones
        /// de persistencia de los pacientes.
        /// </param>
        public RepositorioPacientes(DientesLimpiosDbContext context)
            : base(context)
        {
            // Conserva la referencia al contexto para utilizarlo
            // en las operaciones específicas del repositorio.
            this.context = context;
        }
    }
}
