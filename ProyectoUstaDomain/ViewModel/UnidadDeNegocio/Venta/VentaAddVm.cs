using ProyectoGradoUstaCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    /// <summary>
    /// BasicDependantIntVm Type Info
    /// //Id => IdProducto
    //IdParent => Cantidad
    //Value => Total
    /// </summary>
    public sealed class VentaAddVm
    {
        public short IdCliente { get; set; }        
        public int Abono { get; set; }
        public List<BasicDependantIntVm> Venta { get; set; }
        public bool EsCredito { get; set; }
        public bool ToPrint { get; set; }
        public List<string> InfoPrint { get; set; }

        public VentaAddVm()
        {
            Venta = new List<BasicDependantIntVm>();
            InfoPrint = new List<string>();
        }
    }
}
