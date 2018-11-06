using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaSecurity
{
    public class ConfiguracionMenuTreeParentVm
    {
        public Int16 IdOpcion { get; set; }
        public Int16? IdOpcionPadre { get; set; }
        public string NombreEspanol { get; set; }
        public string NombreIngles { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }
        public Int16? Tipo { get; set; }
        public string NombreTipo { get; set; }
        public Int16 Orden { get; set; }
        public bool Seteo { get; set; }
        public bool IsParent { get; set; }
        public List<ConfiguracionMenuTreeChildVm> Child { get; set; }      
    }
}
