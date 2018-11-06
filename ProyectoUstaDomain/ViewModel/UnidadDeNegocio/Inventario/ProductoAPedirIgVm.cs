using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class ProductoAPedirIgVm
    {
        public int IdRecord { get; set; }
        public int IdProveedor { get; set; }
        public int IdProducto { get; set; }
        public string ProductoNombre { get; set; }
        public string ProveedorNombre { get; set; }
        public string DiaVisita { get; set; }
        public DateTime FechaVisita { get; set; }
        public string DiaEntrega { get; set; }
        public DateTime FechaEntrega { get; set; }
        public bool Confirmado { get; set; }
        public int Qty { get; set; }
        public int Subtotal { get; set; }
    }
}

