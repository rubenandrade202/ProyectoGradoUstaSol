using Infragistics.Web.Mvc;
using ProyectoGradoUstaCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGradoUstaWeb
{
    public sealed class InventarioMainPageVm
    {
        public GridModel GridProveedoresProductosFaltantes { get; set; }
        public GridModel GridproductosAPedir { get; set; }
        public List<BasicStrVm> ProveedoresProyection { get; set; }
        public ComboModel CmbProveedores { get; set; }
        public ComboModel CmbProveedoresProductosPedidosFlt { get; set; }
        public InventarioMainPageVm()
        {
            ProveedoresProyection = new List<BasicStrVm>();            
        }
    }
}