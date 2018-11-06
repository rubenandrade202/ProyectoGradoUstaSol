using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaSecurity
{
    public sealed class BigMenuChildVm
    {        
        public short IdOpcion { get; set; }       
        public string Nombre { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }
        public short? IdTipoOpcionHija { get; set; }
        public string NombreTipoOpcion { get; set; }
        public short Orden { get; set; }        
    }
}
