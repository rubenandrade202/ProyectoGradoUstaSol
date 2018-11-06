using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class ProductoAPedirAddVm
    {
        public int Idproducto { get; set; }
        public short IdProveedor { get; set; }
        public string DiaVisita { get; set; }
        public DateTime FechaVisita { get; set; }
        public int Qty { get; set; }
        public int UsuarioRegistro { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
