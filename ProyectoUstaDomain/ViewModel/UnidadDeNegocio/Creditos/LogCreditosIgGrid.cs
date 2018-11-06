using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class LogCreditosIgGrid
    {
        public int Id { get; set; }
        public int Valor { get; set; }
        public string Cliente { get; set; }
        public string Vendedor { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
