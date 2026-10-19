using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Dominio.Excepciones
{
    /// <summary>
    /// Representa una excepción lanzada cuando se incumple una regla del negocio.
    /// </summary>
    public class ExcepcionDeReglaDeNegocio : Exception
    {
        /// <summary>
        /// Crea una excepción con un mensaje que describe la regla incumplida.
        /// </summary>
        /// <param name="mensaje">Descripción del error ocurrido.</param>
        public ExcepcionDeReglaDeNegocio(string mensaje)
            : base(mensaje)
        {
        }
    }
}