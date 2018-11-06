using Infragistics.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGradoUstaWeb
{
    public sealed class VentaMainPageVm
    {
        public ComboModel CmbVentaUno { get; set; }
        public ComboModel CmbVentaDos { get; set; }
        public ComboModel CmbVentaTres { get; set; }
        public ComboModel CmbVentaCuatro { get; set; }
        public ComboModel CmbClienteVentaUno { get; set; }
        public ComboModel CmbClienteVentaDos { get; set; }
        public ComboModel CmbClienteVentaTres { get; set; }
        public ComboModel CmbClienteVentaCuatro { get; set; }
        public GridModel GmVentaUno { get; set; }
        public GridModel GmVentaDos { get; set; }
        public GridModel GmVentaTres { get; set; }
        public GridModel GmVentaCuatro { get; set; }

        public ComboModel CmbBilleteUno { get; set; }
        public ComboModel CmbBilleteDos { get; set; }
        public ComboModel CmbBilleteTres { get; set; }
        public ComboModel CmbBilleteCuatro { get; set; }
    }
}