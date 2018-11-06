using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class EsquemaGridGenericoVm
    {
        public int Id { get; set; }
        public string ProductoNombre { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public int Subtotal { get; set; }       
    }
}
