//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoUstaDomain
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProductosProyectoUsta
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public short CantidadActual { get; set; }
        public short CantidadUmbral { get; set; }
        public bool Activo { get; set; }
        public Nullable<short> IdUbicacionStock { get; set; }
        public Nullable<short> IdUbicacionNegocio { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public Nullable<System.DateTime> FechaModificado { get; set; }
        public Nullable<int> IdUsuarioModificador { get; set; }
    }
}
