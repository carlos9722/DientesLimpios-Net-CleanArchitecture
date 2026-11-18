using DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Contratos.Repositorios
{
    /// <summary>
    /// Define las operaciones de acceso a datos específicas de los pacientes.
    /// </summary>
    /// <remarks>
    /// Hereda las operaciones genéricas de IRepositorio<Paciente>
    /// y establece las consultas particulares requeridas para la gestión de pacientes.
    /// </remarks>
    public interface IRepositorioPacientes : IRepositorio<Paciente>
    {
        /// <summary>
        /// Obtiene los pacientes que cumplen con los criterios de filtrado y paginación.
        /// </summary>
        /// <param name="filtro">
        /// Criterios de búsqueda y paginación aplicados a los pacientes.
        /// </param>
        /// <returns>
        /// Una colección de pacientes que cumplen con los criterios establecidos.
        /// </returns>
        Task<IEnumerable<Paciente>> ObtenerFiltrado(FiltroPacienteDTO filtro);
    }
}