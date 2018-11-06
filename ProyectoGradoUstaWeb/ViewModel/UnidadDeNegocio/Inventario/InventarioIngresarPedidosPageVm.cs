using Infragistics.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGradoUstaWeb
{
    public sealed class InventarioIngresarPedidosPageVm
    {
        public ComboModel CmbProveedores { get; set; }

        public ComboModel CmbProductosSearch { get; set; }
        public GridModel GridItemsPedidos { get; set; }
        public DatePickerModel FechaPedido { get; set; }
    }
}