using ProyectoGradoUstaCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class PedidosVisitasVm
    {
        public int TotalPagadoDía { get; set; }
        public List<BasicIntDateVm> ListaPedidosPagadosDia { get; set; }

        public List<PedidosVisitasBaseVm> PedidosPorRecibir { get; set; }

        public List<PedidosVisitasBaseVm> PedidosPorConfirmar { get; set; }

        public List<PedidosVisitasBaseVm> Visitas { get; set; }
        //public List<PedidosVisitasBaseVm> Pedidos { get; set; }

        public PedidosVisitasVm()
        {
            ListaPedidosPagadosDia = new List<BasicIntDateVm>();
            //ListaOtrosPedidos = new List<BasicVm>();
            Visitas = new List<PedidosVisitasBaseVm>();
            //Pedidos = new List<PedidosVisitasBaseVm>();
            PedidosPorRecibir = new List<PedidosVisitasBaseVm>();
            PedidosPorConfirmar = new List<PedidosVisitasBaseVm>();
        }
    }
}
