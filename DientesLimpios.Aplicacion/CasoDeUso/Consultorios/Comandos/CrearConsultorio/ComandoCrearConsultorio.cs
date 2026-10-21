using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasoDeUso.Consultorios.Comandos.CrearConsultorio
{
    /// <summary>
    /// Representa la solicitud para crear un consultorio.
    /// </summary>
    public class ComandoCrearConsultorio : IRequest<Guid>
    {
        /// <summary>
        /// Obtiene o establece el nombre del consultorio que se va a crear.
        /// </summary>
        public required string Nombre { get; set; }
    }
}
