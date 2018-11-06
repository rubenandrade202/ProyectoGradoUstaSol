using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class EsqGridGenProductoAInventarioVm
    {
        public int Id { get; set; }
        public string ProductoNombre { get; set; }
        public string UbicacionNombre { get; set; }
        public int IdUbicacion { get; set; }
        public int Qty { get; set; }       
    }
}


