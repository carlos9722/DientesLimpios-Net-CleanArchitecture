
using DientesLimpios.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.Contratos.Repositorios.Modelos
{

    /// <summary>
    /// Representa los criterios de búsqueda para filtrar las citas
    /// según el paciente, dentista, consultorio, estado y rango de fechas.
    /// Los filtros con ? son opcionales y pueden no tener un valor asignado (null).
    /// </summary>
    public class FiltroCitasDTO
    {
        /// <summary>
        /// Identificador del paciente para filtrar sus citas. Es opcional.
        /// </summary>
        public Guid? PacienteId { get; set; }

        /// <summary>
        /// Identificador del dentista para filtrar sus citas. Es opcional.
        /// </summary>
        public Guid? DentistaId { get; set; }

        /// <summary>
        /// Identificador del consultorio para filtrar sus citas. Es opcional.
        /// </summary>
        public Guid? ConsultorioId { get; set; }

        /// <summary>
        /// Estado de la cita utilizado como criterio de búsqueda. Es opcional.
        /// </summary>
        public EstadoCita? EstadoCita { get; set; }

        /// <summary>
        /// Fecha inicial del rango utilizado para filtrar las citas.
        /// </summary>
        public DateTime FechaInicio { get; set; }

        /// <summary>
        /// Fecha final del rango utilizado para filtrar las citas.
        /// </summary>
        public DateTime FechaFin { get; set; }
    }

}
