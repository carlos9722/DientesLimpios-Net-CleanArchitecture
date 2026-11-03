
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio
{
    /// <summary>
    /// Representa el comando encargado de solicitar la actualización
    /// de la información de un consultorio existente.
    /// </summary>
    public class ComandoActualizarConsultorio : IRequest
    {
        /// <summary>
        /// Identificador único del consultorio que se desea actualizar.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nuevo nombre que se asignará al consultorio.
        /// Es un campo obligatorio que debe cumplir las reglas de validación establecidas.
        /// </summary>
        public required string Nombre { get; set; }
    }
}
