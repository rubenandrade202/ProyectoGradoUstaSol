using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Infragistics.Web.Mvc;

namespace ProyectoGradoUstaWeb
{
    public sealed class UbicacionesMainPageVm
    {
        public GridModel GmMainUbicaciones { get; set; }
        public ComboModel CmbTiposUbicacion { get; set; }
    }
}