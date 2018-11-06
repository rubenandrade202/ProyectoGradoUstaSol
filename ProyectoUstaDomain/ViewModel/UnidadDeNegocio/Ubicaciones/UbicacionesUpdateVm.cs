using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class UbicacionesUpdateVm
    {
        public int Id { get; set; }
        public short NewIdTipoUbicacion { get; set; }
        public string NewNombre { get; set; }
        public string NewDescripcion { get; set; }
        public string NewUrlImagen { get; set; }
    }
}
