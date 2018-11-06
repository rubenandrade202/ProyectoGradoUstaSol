using ProyectoGradoUstaCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class PedidosVisitasBaseVm
    {
        public int ValorRegistradoPedido { get; set; }
        public bool Confirmado { get; set; }
        public int IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public List<BasicDependantIntWithNameVm> Records { get; set; }

        public PedidosVisitasBaseVm()
        {
            Records = new List<BasicDependantIntWithNameVm>();
        }
    }
}
