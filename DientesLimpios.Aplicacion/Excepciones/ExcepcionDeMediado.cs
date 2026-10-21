using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion.Excepciones
{
    /// <summary>
    /// Representa una excepción personalizada relacionada con el mediador.
    /// </summary>
    public class ExcepcionDeMediador : Exception
    {
        /// <summary>
        /// Inicializa la excepción con un mensaje descriptivo.
        /// </summary>
        /// <param name="mensaje">Descripción del error ocurrido.</param>
        public ExcepcionDeMediador(string mensaje)
            : base(mensaje)
        {
        }
    }
}

