using Infragistics.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoGradoUstaWeb
{
    public sealed class RealizarInventarioPageVm
    {
        public ComboModel CmbUbicacionesNegocio { get; set; }
        public ComboModel CmbProductosSearch { get; set; }
        public GridModel GridProductosAInventario { get; set; }
    }
}