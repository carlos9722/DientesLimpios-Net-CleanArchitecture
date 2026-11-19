
using DientesLimpios.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Consultas.ObtenerListadoDentistas
{
    /// <summary>
    /// Proporciona métodos de extensión para transformar entidades del dominio en DTO de listado.
    /// </summary>
    /// <remarks>
    /// Centraliza la conversión de la entidad Dentista a DentistaListadoDTO,
    /// permitiendo transferir la información necesaria para mostrar los
    /// registros en el listado sin exponer directamente la entidad del dominio.
    /// </remarks>
    public static class MapeadorExtensions
    {
        /// <summary>
        /// Convierte una entidad Dentista en un objeto de transferencia de datos para el listado.
        /// </summary>
        /// <param name="Dentista">
        /// Entidad del dominio que contiene la información del dentista que se desea transformar.
        /// </param>
        /// <returns>
        /// Un objeto DentistaListadoDTO con el identificador, nombre y correo electrónico del dentista.
        /// </returns>
        /// <remarks>
        /// Extrae los valores de la entidad y los asigna a las propiedades correspondientes
        /// del DTO. El correo electrónico se obtiene mediante la propiedad Valor del objeto de valor Email.
        /// </remarks>
        public static DentistaListadoDTO ADto(this Dentista Dentista)
        {
            // Crea el DTO asignando los datos correspondientes de la entidad Dentista.
            var dto = new DentistaListadoDTO { Id = Dentista.Id, Nombre = Dentista.Nombre, Email = Dentista.Email.Valor };

            // Devuelve el DTO con la información transformada.
            return dto;
        }
    }
}
