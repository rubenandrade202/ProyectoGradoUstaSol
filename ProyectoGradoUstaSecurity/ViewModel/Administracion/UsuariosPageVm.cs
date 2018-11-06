using Infragistics.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaSecurity
{
    public sealed class UsuariosPageVm
    {
        public GridModel Prueba { get; set; }
        public GridModel Usuarios { get; set; }
        public ComboModel Roles { get; set; }
        public ComboModel Idiomas { get; set; }
        public ComboModel UsuariosAd { get; set; }
        public TextEditorModel PassWord { get; set; }
        public TextEditorModel PassWordVerify { get; set; }
        public TextEditorModel Email { get; set; }
        public bool StatusUsersAd { get; set; }

    }
}
