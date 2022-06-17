using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class DTOHeader
    {
        public bool estado { get; set; }
        public string mensaje { get; set; }
        public int id_register;
        public DTOHeader oHeader;
    }
}
