using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaSecurity
{
    public sealed class InfoUsuarioLogOutVm
    {
        public string UserName { get; set; }
        public string NombreRol { get; set; }
        public int IdCurrentIdiom { get; set; }
        public string CurrentIdiom { get; set; }
        public int IdOptionIdiom { get; set; }
        public string NombreOpcionIdiom { get; set; }

    }
}
