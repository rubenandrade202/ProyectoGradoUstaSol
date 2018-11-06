using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class VentasIgRpt
    {
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public int Cantidad { get; set; }
        public string ClienteNombre { get; set; }
        public string ProveedorNombre { get; set; }
        public string VendedorNombre { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int Total { get; set; }
    }
}
