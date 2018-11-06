using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class ProductoPorProveedorVm
    {
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; }
        public int ProveedorId { get; set; }
    }
}
