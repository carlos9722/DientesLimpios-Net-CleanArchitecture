
using DientesLimpios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Persistencia.Configuraciones
{
    /// <summary>
    /// Define la configuración de persistencia para la entidad Dentista.
    /// </summary>
    /// <remarks>
    /// Centraliza las configuraciones de las propiedades de la entidad Dentista,
    /// estableciendo las restricciones y características que se aplicarán
    /// a sus columnas en la base de datos mediante Entity Framework Core.
    /// </remarks>
    public class DentistaConfig : IEntityTypeConfiguration<Dentista>
    {
        /// <summary>
        /// Configura las propiedades de la entidad Dentista para su almacenamiento en la base de datos.
        /// </summary>
        /// <param name="builder">
        /// Constructor de configuración que permite establecer las propiedades,
        /// restricciones y características de persistencia de la entidad.
        /// </param>
        /// <remarks>
        /// Define el nombre como obligatorio con una longitud máxima de 250 caracteres
        /// y configura el objeto de valor Email como una propiedad compleja,
        /// almacenando su valor en una columna denominada Email con una longitud máxima de 254 caracteres.
        /// </remarks>
        public void Configure(EntityTypeBuilder<Dentista> builder)
        {
            // Configura la propiedad Nombre con una longitud máxima de 250 caracteres
            // y establece que su valor es obligatorio en la base de datos.
            builder.Property(prop => prop.Nombre)
                .HasMaxLength(250)
                .IsRequired();

            // Configura Email como una propiedad compleja para almacenar
            // el valor del objeto de valor dentro de la entidad Dentista.
            builder.ComplexProperty(prop => prop.Email, accion =>
            {
                // Asigna el valor del objeto Email a una columna llamada Email
                // y establece una longitud máxima de 254 caracteres.
                accion.Property(e => e.Valor).HasColumnName("Email").HasMaxLength(254);
            });
        }
    }
}
