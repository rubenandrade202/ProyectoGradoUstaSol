using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class MovimientoCajaIgRpt
    {
        public string NombreMovimiento { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public int Total { get; set; }
    }
}
