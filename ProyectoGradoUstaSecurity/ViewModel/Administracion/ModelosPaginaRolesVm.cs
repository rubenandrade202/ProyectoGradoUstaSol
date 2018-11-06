using Infragistics.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaSecurity
{
    public sealed class ModelosPaginaRolesVm
    {
        public GridModel ModeloRoles { get; set; }
        public GridModel ModeloUsuariosByRol { get; set; }
        public TreeGridModel ModeloOpcionesByRol { get; set; }
    }
}
