using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class ProveedorUpdateVm
    {
        public short Id { get; set; }
        public string NewNombre { get; set; }
        public string NewDescripcion { get; set; }
        public string NewNit { get; set; }
        public int? NewTelefono { get; set; }
        public bool NewVisita_Lunes { get; set; }
        public bool NewVisita_Martes { get; set; }
        public bool NewVisita_Miercoles { get; set; }
        public bool NewVisita_Jueves { get; set; }
        public bool NewVisita_Viernes { get; set; }
        public bool NewVisita_Sabado { get; set; }
        public bool NewVisita_Domingo { get; set; }
        public bool NewEntrega_Lunes { get; set; }
        public bool NewEntrega_Martes { get; set; }
        public bool NewEntrega_Miercoles { get; set; }
        public bool NewEntrega_Jueves { get; set; }
        public bool NewEntrega_Viernes { get; set; }
        public bool NewEntrega_Sabado { get; set; }
        public bool NewEntrega_Domingo { get; set; }
    }
}
