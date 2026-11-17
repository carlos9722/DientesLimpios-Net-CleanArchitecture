using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;

namespace DientesLimpios.Dominio.Entidades
{
    /// <summary>
    /// Representa a un dentista dentro del dominio del negocio.
    /// </summary>
    public class Dentista
    {
        // El estado de la entidad se modifica únicamente desde la propia clase.
        public Guid Id { get; private set; }
        /// Indica que el valor se asignará después, evitando advertencias del compilador.
        public string Nombre { get; private set; } = null!;
        /// Indica que el valor se asignará después, evitando advertencias del compilador.
        public Email Email { get; private set; } = null!;

        // <summary>
        /// Inicializa una instancia de la entidad sin parámetros.
        /// </summary>
        /// <remarks>
        /// Constructor privado utilizado por Entity Framework Core
        /// para materializar la entidad al recuperar información de la base de datos.
        /// </remarks>
        private Dentista()
        {
            
        }

        /// <summary>
        /// Crea un dentista validando sus datos obligatorios.
        /// </summary>
        /// <param name="nombre">Nombre del dentista.</param>
        /// <param name="email">Correo electrónico del dentista.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se lanza si el nombre es inválido o el correo es nulo.
        /// </exception>
        public Dentista(string nombre, Email email)
        {
            // El nombre no puede ser nulo, vacío ni contener solo espacios.
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio");
            }

            // El correo electrónico es obligatorio para crear un dentista válido.
            if (email is null)
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} es obligatorio");
            }

            // Asigna un identificador único y establece los datos validados.
            Id = Guid.CreateVersion7();
            Nombre = nombre;
            Email = email;
        }
    }
}