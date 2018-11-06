using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class ProductosBufferInventarioIgVm
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public string ProductoNombre { get; set; }
        public int IdProveedor { get; set; }
        public string ProveedorNombre { get; set; }
        public int QtyPedida { get; set; }
        public int QtyActual { get; set; }
        public int QtyRealAIngresar { get; set; }
        public int CostoItem { get; set; }
        public int PrecioAsignado { get; set; }
    }
}
