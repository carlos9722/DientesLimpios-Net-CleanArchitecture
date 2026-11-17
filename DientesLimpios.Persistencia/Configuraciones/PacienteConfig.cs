using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DientesLimpios.Persistencia.Configuraciones
{
    /// <summary>
    /// Define la configuración de persistencia de la entidad Paciente
    /// mediante Entity Framework Core.
    /// </summary>
    /// <remarks>
    /// Establece las restricciones y características de las propiedades
    /// de Paciente que se utilizarán al generar el modelo de base de datos.
    /// </remarks>
    public class PacienteConfig : IEntityTypeConfiguration<Paciente>
    {
        /// <summary>
        /// Configura las propiedades de la entidad Paciente para su
        /// correspondencia con la estructura de la base de datos.
        /// </summary>
        /// <param name="builder">
        /// Constructor utilizado para definir las propiedades y restricciones
        /// de la entidad Paciente.
        /// </param>
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            // Configura la propiedad Nombre como obligatoria y establece
            // una longitud máxima de 250 caracteres.
            builder.Property(prop => prop.Nombre)
            .HasMaxLength(250)
            .IsRequired();

            // Configura Email como una propiedad de objeto complejo.
            // Su valor se almacena en una columna llamada Email,
            // con una longitud máxima de 254 caracteres.
            builder.ComplexProperty(prop => prop.Email, accion =>
            {
                accion.Property(e => e.Valor).HasColumnName("Email").HasMaxLength(254);
            });

        }
    }
}
