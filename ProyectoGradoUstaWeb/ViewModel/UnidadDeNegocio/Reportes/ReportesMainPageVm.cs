using Infragistics.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGradoUstaWeb
{
    public sealed class ReportesMainPageVm
    {
        public GridModel Gm_Creditos { get; set; }
        public GridModel Gm_LogCreditos { get; set; }        
        public DatePickerModel FechaDesdeMovimientoCaja { get; set; }
        public DatePickerModel FechaHastaMovimientoCaja { get; set; }
        public GridModel Gm_MovimientoCaja { get; set; }
        public DatePickerModel FechaDesdeVentas { get; set; }
        public DatePickerModel FechaHastaVentas { get; set; }
        public GridModel Gm_VentasGral { get; set; }
        public int TotalCostoInventario { get; set; }

    }
}