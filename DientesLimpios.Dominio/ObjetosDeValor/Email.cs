using DientesLimpios.Dominio.Excepciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Dominio.ObjetosDeValor
{
    /// <summary>
    /// Representa un correo electrónico validado como objeto de valor.
    /// </summary>
    public record Email
    {
        // El valor se establece al construir el objeto y no puede modificarse después.
        public string Valor { get; } = null!;

        /// <summary>
        /// Crea un correo electrónico validando que tenga un valor básico válido.
        /// </summary>
        /// <param name="email">Dirección de correo electrónico.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se lanza si el correo está vacío o no contiene el carácter @.
        /// </exception>
        public Email(string email)
        {
            // El correo es obligatorio y no puede contener solo espacios.
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} es obligatorio");
            }

            // Comprueba una condición básica del formato del correo.
            if (!email.Contains("@"))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} no es válido");
            }

            Valor = email;
        }
    }
}