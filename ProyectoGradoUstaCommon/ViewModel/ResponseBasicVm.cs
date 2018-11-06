using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGradoUstaCommon
{
    public sealed class ResponseBasicVm
    {
        public bool Success { get; set; }
        public bool Alert { get; set; }
        public List<string> MessageOk { get; set; }
        public List<string> MessageBad { get; set; }
        public List<string> MessageAlert { get; set; }
        public object Data { get; set; }
        public string UrlSpecific { get; set; }

        public ResponseBasicVm()
        {
            MessageOk = new List<string>();
            MessageBad = new List<string>();
            MessageAlert = new List<string>();
        }
    }
}
