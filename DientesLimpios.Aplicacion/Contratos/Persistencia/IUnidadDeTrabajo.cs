using System;
using System.Collections.Generic;
using System.Text;


namespace DientesLimpios.Aplicacion.Contratos.Persistencia
{
    /// <summary>
    /// Define las operaciones para persistir o revertir cambios pendientes.
    /// </summary>
    public interface IUnidadDeTrabajo
    {
        /// <summary>
        /// Persiste los cambios pendientes en el almacenamiento.
        /// </summary>
        Task Persistir();

        /// <summary>
        /// Revierte los cambios pendientes, según la implementación.
        /// </summary>
        Task Reversar();
    }
}

