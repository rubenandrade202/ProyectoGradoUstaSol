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
    
    public partial class UbicacionesProyectoUsta
    {
        public short Id { get; set; }
        public short IdTipoUbicacion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
        public bool IdEstado { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public int IdUsuarioCreador { get; set; }
        public Nullable<System.DateTime> FechaModificado { get; set; }
        public Nullable<int> IdUsuarioModificador { get; set; }
    }
}
