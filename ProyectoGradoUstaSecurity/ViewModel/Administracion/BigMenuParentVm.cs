using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaSecurity
{
    public sealed class BigMenuParentVm
    {
        public short IdOpcion { get; set; }        
        public string Nombre { get; set; }        
        public short Orden { get; set; }
        public List<BigMenuChildVm> ChildConfiguration { get; set; }
        public List<BigMenuChildVm> ChildProcess { get; set; }
        public List<BigMenuChildVm> ChildReports { get; set; }

        public BigMenuParentVm()
        {
            ChildConfiguration = new List<BigMenuChildVm>();
            ChildProcess = new List<BigMenuChildVm>();
            ChildReports = new List<BigMenuChildVm>();
        }
    }
}
