
using DientesLimpios.Dominio.Excepciones;
using System;

namespace DientesLimpios.Dominio.Entidades
{
    /// <summary>
    /// Representa un consultorio dentro del dominio del negocio y controla
    /// las reglas necesarias para mantener su información válida.
    /// </summary>
    public class Consultorio
    {
        /// <summary>
        /// Identificador único que permite distinguir cada consultorio.
        /// Su valor solo puede establecerse o modificarse desde la propia entidad.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Nombre del consultorio.
        /// Su modificación está controlada por la entidad para garantizar
        /// el cumplimiento de las reglas de negocio.
        /// </summary>
        public string Nombre { get; private set; } = null!;

        /// <summary>
        /// Crea una nueva instancia de consultorio validando que su nombre
        /// cumpla con las reglas de negocio establecidas.
        /// </summary>
        /// <param name="nombre">Nombre que se asignará al consultorio.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se produce cuando el nombre es nulo, vacío o contiene únicamente espacios en blanco.
        /// </exception>
        public Consultorio(string nombre)
        {
            // Verifica que el nombre sea obligatorio y contenga información válida.
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio");
            }

            // Asigna el nombre después de comprobar que cumple la regla de negocio.
            Nombre = nombre;

            // Genera un identificador único para la nueva instancia del consultorio.
            Id = Guid.CreateVersion7();
        }

        /// <summary>
        /// Actualiza el nombre del consultorio después de verificar
        /// que el nuevo valor cumpla con las reglas de negocio.
        /// </summary>
        /// <param name="nombre">Nuevo nombre que se desea asignar al consultorio.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se produce cuando el nombre es nulo, vacío o contiene únicamente espacios en blanco.
        /// </exception>
        public void ActualizarNombre(string nombre)
        {
            // Verifica que el nuevo nombre cumpla las reglas de negocio.
            AplicarReglasDeNegocioNombre(nombre);

            // Actualiza el nombre únicamente después de superar las validaciones.
            Nombre = nombre;
        }

        /// <summary>
        /// Comprueba que el nombre del consultorio cumpla con la regla de negocio
        /// que establece que este campo es obligatorio.
        /// </summary>
        /// <param name="nombre">Nombre que se desea validar.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se produce cuando el nombre es nulo, vacío o contiene únicamente espacios en blanco.
        /// </exception>
        private void AplicarReglasDeNegocioNombre(string nombre)
        {
            // Impide asignar nombres que no contengan información válida.
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio");
            }
        }
    }
}
