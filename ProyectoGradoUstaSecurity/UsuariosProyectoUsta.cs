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
    
    public partial class UsuariosProyectoUsta
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UsuariosProyectoUsta()
        {
            this.UsuariosRolesProyectoUsta = new HashSet<UsuariosRolesProyectoUsta>();
        }
    
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte IdIdioma { get; set; }
        public string Email { get; set; }
        public System.DateTime FechaRegistro { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public Nullable<System.DateTime> FechaModificado { get; set; }
        public Nullable<int> IdUsuarioModificador { get; set; }
        public bool UsuarioEmpresa { get; set; }
        public bool Activo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuariosRolesProyectoUsta> UsuariosRolesProyectoUsta { get; set; }
    }
}
