using ProyectoGradoUstaCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class ProductoAddVm
    {               
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public short CantidadActual { get; set; }
        public short CantidadUmbral { get; set; }        
        public Nullable<short> IdUbicacionStock { get; set; }
        public Nullable<short> IdUbicacionNegocio { get; set; }
        public List<BasicDependantDecimalVm> Proveedores { get; set; }
        public List<string> CodigosBarras { get; set; }

        public ProductoAddVm()
        {
            Proveedores = new List<BasicDependantDecimalVm>();
            CodigosBarras = new List<string>();
        }
    }
}
