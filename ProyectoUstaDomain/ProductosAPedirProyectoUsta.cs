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
    
    public partial class ProductosAPedirProyectoUsta
    {
        public int IdRecord { get; set; }
        public int IdProducto { get; set; }
        public short IdProveedor { get; set; }
        public short Qty { get; set; }
        public System.DateTime FechaVisita { get; set; }
        public string DiaVisita { get; set; }
        public System.DateTime FechaEntrega { get; set; }
        public string DiaEntrega { get; set; }
        public bool Confirmado { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public Nullable<int> IdUsuarioModificador { get; set; }
        public Nullable<System.DateTime> FechaModificado { get; set; }
    }
}
