using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Infragistics.Web.Mvc;

namespace ProyectoGradoUstaWeb
{
    public class ProductoMainPageVm
    {
        public GridModel GridProductos { get; set; }
        public GridModel GridProveedoresProducto { get; set; }
        public GridModel GridCodigosBarrasProducto { get; set; }        
        public ComboModel CmbUbicacionStock { get; set; }
        public ComboModel CmbUbicacionNegocio { get; set; }
        public ComboModel CmbProveedores { get; set; }
    }
}