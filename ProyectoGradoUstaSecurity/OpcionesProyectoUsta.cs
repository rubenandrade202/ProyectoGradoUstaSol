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
    
    public partial class OpcionesProyectoUsta
    {
        public short IdOpcion { get; set; }
        public Nullable<short> IdOpcionPadre { get; set; }
        public string NombreEspanol { get; set; }
        public string NombreIngles { get; set; }
        public short Orden { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }
        public Nullable<short> IdTipoOpcionHija { get; set; }
        public int IdUsuarioCreador { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public Nullable<int> IdUsuarioModificador { get; set; }
        public Nullable<System.DateTime> FechaModificado { get; set; }
    }
}