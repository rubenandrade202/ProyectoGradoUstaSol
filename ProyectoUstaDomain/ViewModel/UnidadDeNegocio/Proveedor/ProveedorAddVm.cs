using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class ProveedorAddVm
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Nit { get; set; }
        public int? Telefono { get; set; }
        public bool Visita_Lunes { get; set; }
        public bool Visita_Martes { get; set; }
        public bool Visita_Miercoles { get; set; }
        public bool Visita_Jueves { get; set; }
        public bool Visita_Viernes { get; set; }
        public bool Visita_Sabado { get; set; }
        public bool Visita_Domingo { get; set; }
        public bool Entrega_Lunes { get; set; }
        public bool Entrega_Martes { get; set; }
        public bool Entrega_Miercoles { get; set; }
        public bool Entrega_Jueves { get; set; }
        public bool Entrega_Viernes { get; set; }
        public bool Entrega_Sabado { get; set; }
        public bool Entrega_Domingo { get; set; }
    }
}
