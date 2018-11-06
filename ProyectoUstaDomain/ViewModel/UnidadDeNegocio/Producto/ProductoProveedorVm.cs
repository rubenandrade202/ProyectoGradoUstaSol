using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class ProductoProveedorVm
    {
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; }
        public int ProveedorId { get; set; }
        public string ProveedorNombre { get; set; }
        public int QtyActual { get; set; }
        public int QtyUmbral { get; set; }
    }
}
