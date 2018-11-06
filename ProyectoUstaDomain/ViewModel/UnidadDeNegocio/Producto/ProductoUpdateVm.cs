using ProyectoGradoUstaCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class ProductoUpdateVm
    {
        public int Id { get; set; }
        public string NewNombre { get; set; }
        public int NewPrecio { get; set; }
        public bool Activo { get; set; }
        public short NewCantidadActual { get; set; }
        public short NewCantidadUmbral { get; set; }
        public Nullable<short> NewIdUbicacionStock { get; set; }
        public Nullable<short> NewIdUbicacionNegocio { get; set; }
        public List<BasicDependantDecimalVm> NewProveedores { get; set; }
        public List<string> NewCodigosBarras { get; set; }

        public ProductoUpdateVm()
        {
            NewProveedores = new List<BasicDependantDecimalVm>();
            NewCodigosBarras = new List<string>();
        }        
    }
}
