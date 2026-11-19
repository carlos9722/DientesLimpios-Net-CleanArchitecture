
using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Aplicacion.CasosDeUso.Dentistas.Comandos.CrearDentista
{
    /// <summary>
    /// Representa la solicitud para registrar un nuevo dentista en la aplicación.
    /// </summary>
    /// <remarks>
    /// Contiene los datos necesarios para crear un dentista y define
    /// el tipo de respuesta que devolverá el manejador mediante su identificador único.
    /// </remarks>
    public class ComandoCrearDentista : IRequest<Guid>
    {
        /// <summary>
        /// Obtiene o establece el nombre del dentista que se desea registrar.
        /// </summary>
        public required string Nombre { get; set; }

        /// <summary>
        /// Obtiene o establece el correo electrónico del dentista que se desea registrar.
        /// </summary>
        public required string Email { get; set; }
    }
}
