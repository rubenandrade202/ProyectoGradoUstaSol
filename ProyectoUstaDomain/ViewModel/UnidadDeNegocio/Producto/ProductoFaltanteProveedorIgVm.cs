using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class ProductoFaltanteProveedorIgVm
    {
        public string IdRecord { get; set; }
        public int ProductoId { get; set; }
        public string ProductoNombre { get; set; }
        public int ProveedorId { get; set; }
        public string ProveedorNombre { get; set; }
        public int CantidadVendida30Dias { get; set; }
        public int CantidadVendida21Dias { get; set; }
        public int CantidadVendida15Dias { get; set; }
        public int CantidadVendida7Dias { get; set; }
        public int CantidadActual { get; set; }
        public int CantidadUmbral { get; set; }
        public int CantidadUmbralSistema { get; set; }
        public int QtyAPedir { get; set; }      
        public int Programado { get; set; }
    }
}
