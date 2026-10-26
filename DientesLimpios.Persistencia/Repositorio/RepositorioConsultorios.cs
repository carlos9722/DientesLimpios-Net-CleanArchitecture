using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Persistencia.Repositorio
{
    /// <summary>
    /// Proporciona acceso a los datos de la entidad Consultorio mediante
    /// las operaciones definidas en el repositorio genérico.
    /// </summary>
    /// <remarks>
    /// Hereda las operaciones comunes de Repositorio<Consultorio>
    /// e implementa el contrato específico IRepositorioConsultorios.
    /// Permite agregar, actualizar, eliminar y consultar consultorios
    /// sin duplicar la lógica de acceso a datos.
    /// </remarks>
    public class RepositorioConsultorios
        : Repositorio<Consultorio>, IRepositorioConsultorios
    {
        /// <summary>
        /// Inicializa una nueva instancia del repositorio de consultorios.
        /// </summary>
        /// <param name="context">
        /// Contexto de Entity Framework Core utilizado para acceder
        /// a los datos de la aplicación.
        /// </param>
        public RepositorioConsultorios(DientesLimpiosDbContext context)
            : base(context)
        {
        }
    }
}
