using DientesLimpios.Dominio.Excepciones;
using System;

namespace DientesLimpios.Dominio.Entidades
{
    /// <summary>
    /// Representa un consultorio dentro del dominio del negocio.
    /// </summary>
    public class Consultorio
    {
        // El identificador y el nombre solo pueden modificarse desde la propia entidad.
        public Guid Id { get; private set; }

        /// Indica que el valor se asignará después, evitando advertencias del compilador.
        public string Nombre { get; private set; } = null!;

        /// <summary>
        /// Crea un consultorio validando que tenga un nombre obligatorio.
        /// </summary>
        /// <param name="nombre">Nombre del consultorio.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se lanza si el nombre es nulo, vacío o contiene solo espacios.
        /// </exception>
        public Consultorio(string nombre)
        {
            // Evita crear consultorios sin un nombre válido.
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio");
            }


            Nombre = nombre;

            // Genera un identificador único para el consultorio.
            Id = Guid.CreateVersion7();
        }
    }
}