
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.BorrarConsultorio
{
    /// <summary>
    /// Representa el comando encargado de solicitar la eliminación
    /// de un consultorio existente.
    /// </summary>
    public class ComandoBorrarConsultorio : IRequest
    {
        /// <summary>
        /// Identificador único del consultorio que se desea eliminar.
        /// </summary>
        public Guid Id { get; set; }
    }
}
