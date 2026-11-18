
using DientesLimpios.Dominio.Excepciones;
using DientesLimpios.Dominio.ObjetosDeValor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Dominio.Entidades
{
    /// <summary>
    /// Representa a un paciente dentro del dominio del negocio.
    /// </summary>
    /// <remarks>
    /// Encapsula la información y las reglas de negocio relacionadas con un paciente,
    /// garantizando que sus datos cumplan las condiciones establecidas por el dominio.
    /// </remarks>
    public class Paciente
    {
        /// <summary>
        /// Identificador único que permite distinguir al paciente dentro del sistema.
        /// </summary>
        /// <remarks>
        /// Su modificación está restringida a la propia entidad para preservar
        /// el encapsulamiento.
        /// </remarks>
        public Guid Id { get; private set; }

        /// <summary>
        /// Nombre del paciente.
        /// </summary>
        /// <remarks>
        /// Su modificación está restringida a la propia entidad para garantizar
        /// que se respeten las reglas de negocio.
        /// </remarks>
        public string Nombre { get; private set; } = null!;

        /// <summary>
        /// Correo electrónico del paciente representado mediante un objeto de valor.
        /// </summary>
        /// <remarks>
        /// Su modificación está restringida a la propia entidad.
        /// </remarks>
        public Email Email { get; private set; } = null!;

        /// <summary>
        /// Inicializa una instancia de la entidad sin parámetros.
        /// </summary>
        /// <remarks>
        /// Constructor privado utilizado por Entity Framework Core
        /// para materializar la entidad al recuperar información de la base de datos.
        /// </remarks>
        private Paciente()
        {

        }

        /// <summary>
        /// Crea una nueva instancia de paciente validando los datos obligatorios
        /// establecidos por las reglas de negocio.
        /// </summary>
        /// <param name="nombre">Nombre del paciente.</param>
        /// <param name="email">Correo electrónico del paciente.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se lanza cuando el nombre está vacío, contiene únicamente espacios
        /// en blanco o el correo electrónico es nulo.
        /// </exception>
        public Paciente(string nombre, Email email)
        {
            // El nombre es obligatorio y no puede contener solo espacios.
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio");
            }

            // El correo debe existir para crear un paciente válido.
            if (email is null)
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} es obligatorio");
            }

            // Genera un identificador único y asigna los datos validados.
            Id = Guid.CreateVersion7();
            Nombre = nombre;
            Email = email;
        }

        /// <summary>
        /// Actualiza el nombre del paciente después de validar
        /// que cumpla las reglas de negocio.
        /// </summary>
        /// <param name="nombre">Nuevo nombre que se asignará al paciente.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se lanza cuando el nombre está vacío o contiene únicamente espacios en blanco.
        /// </exception>
        public void ActualizarNombre(string nombre)
        {
            // Valida el nuevo nombre antes de modificar el estado de la entidad.
            AplicarReglasDeNegocioNombre(nombre);

            // Asigna el nuevo nombre una vez superadas las validaciones.
            Nombre = nombre;
        }

        /// <summary>
        /// Aplica las reglas de negocio relacionadas con el nombre del paciente.
        /// </summary>
        /// <param name="nombre">Nombre que se desea validar.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se lanza cuando el nombre es nulo, vacío o contiene únicamente espacios en blanco.
        /// </exception>
        private void AplicarReglasDeNegocioNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(nombre)} es obligatorio");
            }
        }

        /// <summary>
        /// Actualiza el correo electrónico del paciente después de validar
        /// que el objeto de valor proporcionado sea válido.
        /// </summary>
        /// <param name="email">Nuevo correo electrónico que se asignará al paciente.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se lanza cuando el correo electrónico es nulo.
        /// </exception>
        public void ActualizarEmail(Email email)
        {
            // Valida el correo electrónico antes de modificar el estado de la entidad.
            AplicarReglasDeNegocioEmail(email);

            // Asigna el nuevo correo una vez superadas las validaciones.
            Email = email;
        }

        /// <summary>
        /// Aplica las reglas de negocio relacionadas con el correo electrónico del paciente.
        /// </summary>
        /// <param name="email">Objeto de valor que contiene el correo electrónico que se desea validar.</param>
        /// <exception cref="ExcepcionDeReglaDeNegocio">
        /// Se lanza cuando el objeto de valor del correo electrónico es nulo.
        /// </exception>
        private void AplicarReglasDeNegocioEmail(Email email)
        {
            if (email is null)
            {
                throw new ExcepcionDeReglaDeNegocio($"El {nameof(email)} es obligatorio");
            }
        }
    }
}
