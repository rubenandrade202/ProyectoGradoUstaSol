//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProyectoGradoUstaSecurity
{
    using System;
    using System.Collections.Generic;
    
    public partial class OpcionesUsuarioProyectoUsta
    {
        public short IdOpcion { get; set; }
        public short IdUsuario { get; set; }
        public bool Actualizar { get; set; }
        public bool Consultar { get; set; }
        public bool Imprimir { get; set; }
        public bool Exportar { get; set; }
        public int IdUsuarioCreador { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public Nullable<int> IdUsuarioModificador { get; set; }
        public Nullable<System.DateTime> FechaModificado { get; set; }
    }
}