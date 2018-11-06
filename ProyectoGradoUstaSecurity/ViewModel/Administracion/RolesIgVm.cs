using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaSecurity
{
    public sealed class RolesIgVm
    {
        public short IdRol { get; set; }
        public string NombreRol { get; set; }
        public bool IdEstado { get; set; }
        public int IdUsuarioCreador { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int? IdUsuarioModificador { get; set; }
        public DateTime? FechaModificado { get; set; }
        public string NombreUsuarioCreador { get; set; }
        public string NombreUsuarioModificador { get; set; }
    }
}
