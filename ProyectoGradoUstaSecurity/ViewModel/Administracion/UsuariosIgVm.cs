using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaSecurity
{
    public sealed class UsuariosIgVm
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public short IdIdioma { get; set; }
        public string NombreIdioma { get; set; }
        public string Email { get; set; }
        public int IdRol { get; set; }
        public string NombreRol { get; set; }
        public bool UsuarioElite { get; set; }
        public bool MenuEspecifico { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaModificado { get; set; }
    }
}
