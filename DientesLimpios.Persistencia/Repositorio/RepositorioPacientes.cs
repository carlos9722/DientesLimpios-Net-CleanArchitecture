using DientesLimpios.Aplicacion.CasoDeUso.Pacientes.Consultas.ObtenerListadoPacientes;
using DientesLimpios.Aplicacion.Contratos.Repositorios;
using DientesLimpios.Dominio.Entidades;
using DientesLimpios.Persistencia.Utilidades;
using Microsoft.EntityFrameworkCore;
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
        /// Contexto de Entity Framework Core utilizado para acceder
        /// a los datos de los pacientes.
        /// </summary>
        private readonly DientesLimpiosDbContext context;

        /// <summary>
        /// Inicializa una nueva instancia del repositorio de pacientes.
        /// </summary>
        /// <param name="context">
        /// Contexto de base de datos utilizado para realizar las operaciones
        /// de persistencia.
        /// </param>
        public RepositorioPacientes(DientesLimpiosDbContext context)
            : base(context)
        {
            // Conserva la referencia al contexto para utilizarlo
            // en las operaciones específicas del repositorio.
            this.context = context;
        }

        /// <summary>
        /// Obtiene los pacientes que cumplen con los criterios de filtrado
        /// y paginación especificados.
        /// </summary>
        /// <param name="filtro">
        /// Criterios de búsqueda y paginación aplicados al listado de pacientes.
        /// </param>
        /// <returns>
        /// Una colección de pacientes que cumplen con los filtros establecidos,
        /// ordenada por nombre y limitada a la página solicitada.
        /// </returns>
        public async Task<IEnumerable<Paciente>> ObtenerFiltrado(FiltroPacienteDTO filtro)
        {
            // Inicializa la consulta de pacientes para aplicar filtros dinámicamente.
            var queryable = context.Pacientes.AsQueryable();

            // Filtra los pacientes por nombre cuando se proporciona un valor.
            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
            {
                queryable = queryable.Where(x => x.Nombre.Contains(filtro.Nombre));
            }

            // Filtra los pacientes por correo electrónico cuando se proporciona un valor.
            if (!string.IsNullOrWhiteSpace(filtro.Email))
            {
                queryable = queryable.Where(x => x.Email.Valor.Contains(filtro.Email));
            }

            // Ordena los resultados por nombre y aplica la paginación solicitada.
            return await queryable.OrderBy(x => x.Nombre)
                .Paginar(filtro.Pagina, filtro.RegistrosPorPagina).ToListAsync();
        }
    }
}