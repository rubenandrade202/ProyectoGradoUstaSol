using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaSecurity
{
    public sealed class MenuTreeByRolFromSp
    {
        public short IdOpcion { get; set; }
        public short? IdOpcionPadre { get; set; }
        public string NombreEspanol { get; set; }
        public string NombreIngles { get; set; }
        public short? IdRol { get; set; }
    }
}
