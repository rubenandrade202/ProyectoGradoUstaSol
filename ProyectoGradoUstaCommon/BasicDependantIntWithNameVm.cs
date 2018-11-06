using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaCommon
{
    public sealed class BasicDependantIntWithNameVm
    {
        public int IdParent { get; set; }
        public string NameParent { get; set; }
        public int Id { get; set; }
        public string NameId { get; set; }
        public int Value { get; set; }
        public string NameValue { get; set; }
    }
}
