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
    
    public partial class ClientesProyectoUsta
    {
        public short Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int CupoAsignado { get; set; }
        public int CupoEmpleado { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public Nullable<int> IdUsuarioModificado { get; set; }
        public Nullable<System.DateTime> FechaModificado { get; set; }
    }
}
