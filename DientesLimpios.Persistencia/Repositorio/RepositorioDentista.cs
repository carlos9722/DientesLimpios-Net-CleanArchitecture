
using DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentistas;
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
    /// Implementa las operaciones de acceso a datos relacionadas con la entidad Dentista.
    /// </summary>
    /// <remarks>
    /// Hereda las operaciones genéricas del repositorio base y define
    /// consultas específicas para recuperar dentistas mediante criterios
    /// de filtrado y paginación utilizando Entity Framework Core.
    /// </remarks>
    public class RepositorioDentista : Repositorio<Dentista>, IRepositorioDentista
    {
        // Contexto de Entity Framework Core utilizado para consultar los dentistas en la base de datos.
        private readonly DientesLimpiosDbContext context;

        /// <summary>
        /// Inicializa una instancia del repositorio de dentistas.
        /// </summary>
        /// <param name="context">
        /// Contexto de Entity Framework Core encargado de gestionar el acceso a la base de datos.
        /// </param>
        /// <remarks>
        /// Utiliza el contexto recibido para inicializar el repositorio base
        /// y realizar las operaciones específicas de la entidad Dentista.
        /// </remarks>
        public RepositorioDentista(DientesLimpiosDbContext context)
                : base(context)
        {
            this.context = context;
        }

        /// <summary>
        /// Recupera los dentistas aplicando criterios de filtrado y paginación.
        /// </summary>
        /// <param name="filtro">
        /// Objeto que contiene los criterios de búsqueda y los parámetros de paginación.
        /// </param>
        /// <returns>
        /// Una tarea que representa la ejecución asíncrona y devuelve una colección
        /// de entidades Dentista que cumplen con los criterios establecidos.
        /// </returns>
        /// <remarks>
        /// Permite filtrar los dentistas por nombre y correo electrónico,
        /// ordenar los resultados alfabéticamente por nombre y aplicar
        /// la paginación antes de ejecutar la consulta en la base de datos.
        /// </remarks>
        public async Task<IEnumerable<Dentista>> ObtenerFiltrado(FiltroDentistasDTO filtro)
        {
            // Inicializa una consulta sobre el conjunto de dentistas sin ejecutarla todavía.
            var queryable = context.Dentistas.AsQueryable();

            // Aplica el filtro por nombre cuando se proporciona un valor de búsqueda.
            if (!string.IsNullOrWhiteSpace(filtro.Nombre))
            {
                queryable = queryable.Where(x => x.Nombre.Contains(filtro.Nombre));
            }

            // Aplica el filtro por correo electrónico cuando se proporciona un valor de búsqueda.
            // Accede a la propiedad Valor del objeto de valor Email para realizar la comparación.
            if (!string.IsNullOrWhiteSpace(filtro.Email))
            {
                queryable = queryable.Where(x => x.Email.Valor.Contains(filtro.Email));
            }

            // Ordena los resultados por nombre, aplica la paginación
            // y ejecuta la consulta de forma asíncrona para obtener la lista.
            return await queryable.OrderBy(x => x.Nombre).Paginar(filtro.Pagina, filtro.RegistrosPorPagina).ToListAsync();
        }
    }
}
