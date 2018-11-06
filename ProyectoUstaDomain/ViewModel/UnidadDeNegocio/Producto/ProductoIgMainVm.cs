using ProyectoGradoUstaCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class ProductoIgMainVm
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Precio { get; set; }
        public short CantidadActual { get; set; }
        public short CantidadUmbral { get; set; }       
        public short? IdUbicacionStock { get; set; }
        public string NombreUbicacionStock { get; set; }
        public short? IdUbicacionNegocio { get; set; }
        public string NombreUbicacionNegocio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int IdUsuarioRegistro { get; set; }
        public string NombreUsuarioRegistro { get; set; }
        public DateTime? FechaModificado { get; set; }
        public int? IdUsuarioModificador { get; set; }
        public string NombreUsuarioModificador { get; set; }
        public List<BasicDependantDecimalVm> Proveedores { get; set; }

        public List<string> CodigosBarras { get; set; }
        public ProductoIgMainVm()
        {
            Proveedores = new List<BasicDependantDecimalVm>();
            CodigosBarras = new List<string>();
        }
    }
}
