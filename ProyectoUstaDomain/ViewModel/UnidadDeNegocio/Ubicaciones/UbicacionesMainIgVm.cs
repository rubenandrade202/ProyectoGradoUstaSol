using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class UbicacionesMainIgVm
    {
        public short Id { get; set; }
        public string NombreUbicacion { get; set; }
        public short IdTipoUbicacion { get; set; }
        public string NombreTipoUbicacion { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }        
        public DateTime FechaRegistro { get; set; }
        public string NombreUsuarioCreador{ get; set; }
        public DateTime? FechaModificado { get; set; }
        public string NombreUsuarioModificador { get; set; }
    }
}
