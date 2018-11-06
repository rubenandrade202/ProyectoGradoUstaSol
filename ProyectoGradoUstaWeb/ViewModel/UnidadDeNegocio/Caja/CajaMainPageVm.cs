using Infragistics.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGradoUstaWeb
{
    public class CajaMainPageVm
    {
        public GridModel GmCaja { get; set; }
        public ComboModel CmbMovimiento { get; set; }
        public ComboModel CmbDevolucion { get; set; }
        public DatePickerModel FechaInicioFlt { get; set; }
        public DatePickerModel FechaFinFlt { get; set; }
    }
}