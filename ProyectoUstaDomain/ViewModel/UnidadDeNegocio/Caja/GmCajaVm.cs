using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoUstaDomain
{
    public sealed class GmCajaVm
    {
        public int Id { get; set; }
        public int IdTipoMovimiento { get; set; }
        public string NombreTipoMovimiento { get; set; }
        public int Valor { get; set; }
        public DateTime FechaRegistro { get; set; }       
    }
}
