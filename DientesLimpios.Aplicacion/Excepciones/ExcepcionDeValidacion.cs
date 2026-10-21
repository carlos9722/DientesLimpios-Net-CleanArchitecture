using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion.Excepciones
{
    /// <summary>
    /// Representa una excepción que contiene errores de validación.
    /// </summary>
    public class ExcepcionDeValidacion : Exception
    {
        /// <summary>
        /// Obtiene o establece los mensajes de los errores de validación.
        /// </summary>
        public List<string> ErroresDeValidacion { get; set; } = [];

        /// <summary>
        /// Inicializa la excepción a partir del resultado de una validación.
        /// </summary>
        /// <param name="validationResult">
        /// Resultado que contiene los errores detectados durante la validación.
        /// </param>
        public ExcepcionDeValidacion(ValidationResult validationResult)
        {
            // Recopila los mensajes de los errores para facilitar su consulta.
            foreach (var errorDeValidacion in validationResult.Errors)
            {
                ErroresDeValidacion.Add(errorDeValidacion.ErrorMessage);
            }
        }
    }
}

