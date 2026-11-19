
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;

namespace DientesLimpios.Dominio.Entidades
{
    /// <summary>
    /// Representa a un dentista dentro del dominio del negocio.
    /// </summary>
    /// <remarks>
    /// Contiene la información del dentista y controla las reglas de negocio
    /// necesarias para garantizar la validez de sus datos.
    /// </remarks>
    public class Dentista
    {
        // El estado de la entidad se modifica únicamente desde la propia clase.
        public Guid Id { get; private set; }

        /// Indica que el valor se asignará después, evitando advertencias del compilador.
        public string Nombre { get; private set; } = null!;

        /// Indica que el valor se asignará después, evitando advertencias del compilador.
        public Email Email { get; private set; } = null!;

        /// <summary>
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
        /// Crea una instancia de la entidad Dentista validando sus datos obligatorios.
        /// </summary>
        /// <param name="nombre">Nombre del dentista.</param>
        /// <param name="email">Correo electrónico del dentista.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se lanza cuando el nombre es inválido o el correo electrónico es nulo.
        /// </exception>
        /// <remarks>
        /// Genera un identificador único y asigna los datos después de validar
        /// las reglas de negocio correspondientes.
        /// </remarks>
        public Dentista(string nombre, Email email)
        {
            AplicarReglasDeNegocioNombre(nombre);
            AplicarReglasDeNegocioEmail(email);

            // Asigna un identificador único y establece los datos validados.
            Id = Guid.CreateVersion7();
            Nombre = nombre;
            Email = email;
        }

        /// <summary>
        /// Actualiza el nombre del dentista después de validar su contenido.
        /// </summary>
        /// <param name="nombre">Nuevo nombre del dentista.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se lanza cuando el nombre es nulo, vacío o contiene únicamente espacios en blanco.
        /// </exception>
        public void ActualizarNombre(string nombre)
        {
            AplicarReglasDeNegocioNombre(nombre);
            Nombre = nombre;
        }

        /// <summary>
        /// Valida que el nombre proporcionado cumpla las reglas de negocio.
        /// </summary>
        /// <param name="nombre">Nombre que se desea validar.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se lanza cuando el nombre es nulo, vacío o contiene únicamente espacios en blanco.
        /// </exception>
        /// <remarks>
        /// Este método centraliza la validación para reutilizarla
        /// durante la creación y actualización del dentista.
        /// </remarks>
        private void AplicarReglasDeNegocioNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio");
            }
        }

        /// <summary>
        /// Actualiza el correo electrónico del dentista después de validar su valor.
        /// </summary>
        /// <param name="email">Nuevo correo electrónico del dentista.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se lanza cuando el correo electrónico es nulo.
        /// </exception>
        public void ActualizarEmail(Email email)
        {
            AplicarReglasDeNegocioEmail(email);
            Email = email;
        }

        /// <summary>
        /// Valida que el correo electrónico proporcionado no sea nulo.
        /// </summary>
        /// <param name="email">Correo electrónico que se desea validar.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se lanza cuando el correo electrónico es nulo.
        /// </exception>
        /// <remarks>
        /// Centraliza la validación del correo electrónico para reutilizarla
        /// durante la creación y actualización del dentista.
        /// </remarks>
        private void AplicarReglasDeNegocioEmail(Email email)
        {
            if (email is null)
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} es obligatorio");
            }
        }
    }
}
