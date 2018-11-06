using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class UbicacionesAddVm
    {
        public short IdTipoUbicacion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
    }
}
