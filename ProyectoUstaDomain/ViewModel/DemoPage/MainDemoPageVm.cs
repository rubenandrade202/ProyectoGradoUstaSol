using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infragistics.Web.Mvc;

namespace ProyectoUstaDomain
{
    public sealed class MainDemoPageVm
    {
        public ComboModel CmbNormal { get; set; }
        public ComboModel CmbRemoto { get; set; }
        public DatePickerModel DpkPrueba { get; set; }
    }
}
